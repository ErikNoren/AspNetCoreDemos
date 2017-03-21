using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Services
{
    public static class AsyncWorkerServiceExtensions
    {
        public static IServiceCollection AddAsyncWorker(this IServiceCollection services, Action<AsyncWorkerServiceOptions> setupAction = null)
        {
            var settings = new AsyncWorkerServiceOptions();
            setupAction?.Invoke(settings);
            
            return services.AddSingleton<AsyncWorkerService>(svc => {
                var logService = svc.GetService<ILogger<AsyncWorkerService>>();
                var lifetime = svc.GetService<IApplicationLifetime>();

                return new AsyncWorkerService(settings, logService, lifetime);
            });
        }
    }
}
