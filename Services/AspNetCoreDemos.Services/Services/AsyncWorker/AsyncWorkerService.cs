using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Services
{
    public class AsyncWorkerService: IDisposable
    {
        private readonly AsyncWorkerServiceOptions _settings;
        private readonly ILogger<AsyncWorkerService> _logger;
        private readonly IApplicationLifetime _appLifetime;

        private readonly CancellationToken _shutdownToken;

        private readonly BlockingCollection<Action> _workQueue;
        private readonly Task _workerTask;
        private long _workItemQueuedCount = 0;
        private long _workItemProcessedCount = 0;

        public AsyncWorkerService(AsyncWorkerServiceOptions settings, ILogger<AsyncWorkerService> logger, IApplicationLifetime appLifetime)
        {
            _settings = settings;
            _logger = logger;
            _appLifetime = appLifetime;

            _shutdownToken = appLifetime.ApplicationStopping;
            _shutdownToken.Register(AlertShutdown);

            _workQueue = new BlockingCollection<Action>();
            _workerTask = Task.Factory.StartNew(ProcessWork, CancellationToken.None, TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }


        public void QueueWork(Action workItem)
        {
            if (workItem == null) throw new ArgumentNullException(nameof(workItem));

            _workQueue.Add(workItem);
            Interlocked.Increment(ref _workItemQueuedCount);
        }

        public bool TryQueueWork(Action workItem)
        {
            try
            {
                QueueWork(workItem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AlertShutdown()
        {
            _logger.LogInformation($"{nameof(AsyncWorkerService)} {nameof(AlertShutdown)}: We're being told the application is shutting down gracefully.");
        }

        public void InvokeAction(Action workItem)
        {
            _logger.LogInformation($"{nameof(AsyncWorkerService)} {nameof(InvokeAction)}: Starting work item.");

            workItem?.Invoke();
            Interlocked.Increment(ref _workItemProcessedCount);

            _logger.LogInformation($"{nameof(AsyncWorkerService)} {nameof(InvokeAction)}: ");
        }
        
        private void ProcessWork()
        {
            try
            {
                try
                {
                    while (true)
                    {
                        //We can't use the cancellation token in the while() clause since we block while waiting for an event to process
                        //If no event comes in after we've cancelled, we will get stuck at Take() and fail to shut down
                        var next = _workQueue.Take(_shutdownToken);
                        InvokeAction(next);
                    }
                }
                catch (OperationCanceledException) //We're shutting down and have stopped waiting for events. We'll try to flush any remaining
                {
                    _logger.LogInformation($"{nameof(AsyncWorkerService)} {nameof(ProcessWork)} observed the application is shutting down. Flushing remaining work items. Queued work item count: {_workQueue.Count}");

                    Action next;
                    while (_workQueue.TryTake(out next))
                        InvokeAction(next);

                    _logger.LogInformation($"{nameof(AsyncWorkerService)} {nameof(ProcessWork)}: Lifetime Work Items: Received {_workItemQueuedCount}, Processed {_workItemProcessedCount}.");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var remainingItems = _workQueue.Count;

                    if (remainingItems > 0)
                    {
                        _logger.LogCritical(-1000, ex,
                            $"{nameof(AsyncWorkerService)} {nameof(ProcessWork)} encountered fatal error and is in a broken state. {remainingItems} will not be processed. In shutdown process: {_shutdownToken.IsCancellationRequested}.");
                    }

                    if (!_shutdownToken.IsCancellationRequested)
                    {
                        _logger.LogCritical($"{nameof(AsyncWorkerService)}: Application shutdown cancellation token is NOT set. Setting work queue to completed to prevent new work items from being queued.");

                        _workQueue.CompleteAdding();
                    }
                }
                catch
                {
                    _logger.LogCritical(-1000, ex,
                        $"{nameof(AsyncWorkerService)} {nameof(ProcessWork)} encountered fatal error and is in a broken state. Work will not be further processed. Blocking new requests. In shutdown process: {_shutdownToken.IsCancellationRequested}");
                }
            }
        }

        public void Dispose()
        {
            //Wait for work to finish
            _workerTask.Wait();

            _workQueue.Dispose();
        }
    }
}
