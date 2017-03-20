using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Middleware.Simple
{
    public class RandomNumberResponseHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public RandomNumberResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        
        public async Task Invoke(HttpContext httpContext)
        {
            //Request Is Traversing Middleware

            //By the time we get the response after _next.Invoke the response might already be partially sent
            //If we try to set the response headers after _next.Invoke it's likely too late; instead use a callback
            httpContext.Response.OnStarting(() =>
            {
                return Task.Run(() =>
                {
                    httpContext.Response.Headers.Add(
                        "X-Random-Number", new Random().Next().ToString());
                });
            });

            //Request Is Passed Down

            await _next?.Invoke(httpContext);
            
            //Response Is Being Constructed and Possibly Already Partially Transmitted


            //Response Is Passed Up
        }
    }
}
