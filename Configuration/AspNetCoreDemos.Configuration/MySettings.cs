using System;
using System.Collections.Generic;

namespace AspNetCoreDemos.Configuration
{
    public class MySettings
    {
        public string ApplicationVersion { get; set; }
        public Guid ApplicationIdentification { get; set; }
        public AutocompleteSettings AutocompleteOptions { get; set; }
        public IEnumerable<Administrator> Administrators { get; set; }

        public class AutocompleteSettings
        {
            public int MaxResults { get; set; } = 100;
            public bool IncludeInactives { get; set; } = true;
            public IEnumerable<string> DefaultGroceryOptions { get; set; }
        }
        
        public class Administrator
        {
            public int Id { get; set; }
            public string DisplayName { get; set; }
            public string Email { get; set; }
        }
    }
}
