
----------------------------
--   ProjectReferencing   --  
-- Erik Noren 13 Feb 2017 --
----------------------------

Purpose: To demonstrate how to reference projects from your ASP.NET Core application.

This demo is split into two nearly identical parts each under a different Solution Folder.
Both demos contain a .Data and a .Web project. The .Web project is an ASP.NET Core application with
either .NET Framework or .NET Core as shown by the solution folder names NetFx and NetCore, respectively.

In the NetFx solution, the .Data project is a common Class Library project.
In the NetCore solution, the .Data project is a Class Library (Portable) project targeting to .NET Standard.

Aside from the .Data project types, the solutions are practically identical.