
----------------------------
--        Logging         --  
-- Erik Noren 13 Feb 2017 --
-- Edited 19 Mar 2017     --
----------------------------

Purpose: Demonstrate the abstraction of the loggerFactory for use in code independent of choice of logging library.

Key takeaway: When using the Dependency Service it is very easy to add logging to your code using the
logging abstractions - ILogger<T> for example. Doing this allows for the possibility to alter the logging
provider your application uses, customize what minimum level of log message is retained, etc.

The projects in BasicLogging and Serilog are practically the same with the only exception being in the Startup.
The BasicLogging project makes use of the templated loggerFactory calls to set up logging to a console window
and the debugger. In the Serilog project a few Serilog NuGet packages are added and the loggerFactory calls are
replaced with Serilog specific configuration followed by a loggerFactory call to let the framework know where
to route its log messages.
