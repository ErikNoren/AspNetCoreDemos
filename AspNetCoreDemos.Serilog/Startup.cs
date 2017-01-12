using AspNetCoreDemos.SerilogLogging.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace AspNetCoreDemos.SerilogLogging
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MockDatastore>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logFilename = System.IO.Path.Combine(env.ContentRootPath, "logs", $"{env.ApplicationName}-{DateTime.Now.ToString("yyyyMMdd")}_log.txt");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .WriteTo.File(logFilename)
                .CreateLogger();

            loggerFactory.AddSerilog();
            
            app.UseMvc();
        }
    }
}
