using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Middleware.ActionConfigured
{
    public static class ConfigurableRandomNumberGeneratorHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseConfigurableRandomNumberReponseHeader(this IApplicationBuilder builder, Action<ConfigurableRandomNumberGeneratorHeaderMiddlewareSettings> options)
        {
            var settings = new ConfigurableRandomNumberGeneratorHeaderMiddlewareSettings();
            options?.Invoke(settings);

            return builder.UseMiddleware<ConfigurableRandomNumberGeneratorHeaderMiddleware>(settings);
        }
    }
}
