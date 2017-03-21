
----------------------------
--        Services        --  
-- Erik Noren 20 Mar 2017 --
----------------------------

Purpose: To show how one might leverage application services to do work
  that needs more than a single request/response cycle to do work.

Key takeaway: Adding services to the dependency service container makes
it available for injection into other classes. In this demo we create
an extension method to push the user to adding the service as a singleton
which is the desired scenario. Notice by explicitly configuring the service
container we must pass in the dependencies explicitly but they are easily
retrieved from the service container quite easily.