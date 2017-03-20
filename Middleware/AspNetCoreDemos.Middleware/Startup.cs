using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using AspNetCoreDemos.Middleware.Simple;
using AspNetCoreDemos.Middleware.ActionConfigured;

namespace AspNetCoreDemos.Middleware
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        //Build the DI Container - register anything injectable
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        //Define the order of middleware that will handle incoming requests; some middleware can short-circut
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseRandomNumberReponseHeader();
            app.UseConfigurableRandomNumberReponseHeader(options => {
                options.ResponseHeaderName = "X-pRNG";
                options.MinimumNumber = -5;
                options.MaxNumber = 5;
            });

            app.UseMvc();
        }
    }
}
