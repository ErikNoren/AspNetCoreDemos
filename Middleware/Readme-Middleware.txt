
----------------------------
--     Configuration      --  
-- Erik Noren 13 Feb 2017 --
-- Edited 19 Mar 2017     --
----------------------------

Purpose: To demonstrate the configuration providers and retrieving values.

This demo shows how multiple sources of configuration information can be
collected together into a single IConfiguration object. These configuration
values can be retrieved directly by name or by mapping a configuration section
to a POCO object.

Key takeaway: Regardless of how the configuration source data is specified
(JSON files, Environment Variables, INI files, etc.), all the values are
merged into a single object with a consistent encoding style. This abstraction
makes it easy to override settings with different values even if they are
provided through a different format and provider as long as the full name
translates the same way.

For example, a JSON configuration file like the following
{
   "Autocomplete": {
      "MaxResults": 25
   }
}

Would be represented as a key value pair where the key would be
"Autocomplete:MaxResults" with a value of "25". Knowing that, we can override
this setting with an Environment Variable whose key was "Autocomplete:MaxResults"
as long as the Environment Variable provider was specified after the JSON file
provider in the configuration builder in your Startup. The order of the providers
is significant with each new provider layering on top of the previous one.

This key value pair representation using colons as a separator means we can also
add new properties into a containing object. For example, assume the provider
loading order first loads appsettings.json followed by appsettings.Development.json

appsettings.json
{
   "ConnectionStrings": {
      "WarehouseConnection": "Common Connection String For Most Environments"
   }
}

appsettings.Development.json
{
   "ConnectionStrings": {
      "ApplicationDb": "Application Database Connection String",
	  "WarehouseConnection": "Development Environment Specific Change"
   }
}

In a "Development" environment (ASPNETCORE_ENVIRONMENT = "Development") the
IConfiguration object would know of 2 connection strings:
ApplicationDb = "Application Database Connection String"
WarehouseConnection = "Development Environment Specific Change"

In any other environment the IConfiguration object would know of 1 connection string:
WarehouseConnection = "Common Connection String For Most Environments"

This serialization makes it easy to spread configuration values across providers as
makes sense and still be able to map them to a POCO object.