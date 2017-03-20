using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemos.Middleware.ActionConfigured
{
    public class ConfigurableRandomNumberGeneratorHeaderMiddlewareSettings
    {
        public string ResponseHeaderName { get; set; } = "X-Advanced-Random-Number";
        public int MinimumNumber { get; set; } = 0;
        public int MaxNumber { get; set; } = int.MaxValue;

        public ConfigurableRandomNumberGeneratorHeaderMiddlewareSettings()
        { }
    }
}
