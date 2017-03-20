using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Middleware.Simple
{
    public static class RandomNumberResponseHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseRandomNumberReponseHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RandomNumberResponseHeaderMiddleware>();
        }
    }
}
