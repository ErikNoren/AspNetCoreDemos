
----------------------------
--   ProjectReferencing   --  
-- Erik Noren 13 Feb 2017 --
-- Edited 19 Mar 2017     --
----------------------------

Purpose: To demonstrate how to reference projects from your ASP.NET Core application.

This demo is split into two nearly identical parts each under a different Solution Folder.
Both demos contain a .Data and a .Web project. The .Web project is an ASP.NET Core application with
either .NET Framework or .NET Core as shown by the solution folder names NetFx and NetCore, respectively.

In the NetFx solution, the .Data project is a common Class Library project.
In the NetCore solution, the .Data project is a Class Library (Portable) project targeting to .NET Standard.

Aside from the .Data project types, the solutions are practically identical.

Key takeaway: Platform code is nearly the same between .NET Framework and .NET Core for common situations.
While the tooling could use some improvement especially around creating libraries that can be cross-compiled
for both .NET Framework and .NET Core the overall effort in developing for .NET Core isn't much different
from .NET Framework and comes with the benefit of being able to be run in different environments without changes.