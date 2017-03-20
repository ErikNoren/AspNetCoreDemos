using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Middleware.ActionConfigured
{
    public class ConfigurableRandomNumberGeneratorHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConfigurableRandomNumberGeneratorHeaderMiddlewareSettings _settings;

        public ConfigurableRandomNumberGeneratorHeaderMiddleware(RequestDelegate next, ConfigurableRandomNumberGeneratorHeaderMiddlewareSettings settings)
        {
            _next = next;
            _settings = settings;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var multiplier = _settings.MaxNumber - _settings.MinimumNumber;
            var rand = Math.Round(multiplier * new Random().NextDouble() + _settings.MinimumNumber);
            
            httpContext.Response.OnStarting(() => Task.Run(() => {
                httpContext.Response.Headers.Add(_settings.ResponseHeaderName, rand.ToString());
            }));

            await _next?.Invoke(httpContext);
        }
    }
}
