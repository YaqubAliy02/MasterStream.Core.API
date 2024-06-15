//------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use Comfort and Peace
//-----------------------------

using System.Collections.Generic;
using System.IO;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var aDotNetClient = new ADotNetClient();

var githubPipeline = new GithubPipeline
{
    Name = "Github",

    OnEvents = new Events
    {
        Push = new PushEvent
        {
            Branches = new string[] { "main" }
        },

        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "main" }
        }
    },

    Jobs = new Dictionary<string, Job>
      {
          {
              "build",
              new Job
              {
                  RunsOn = BuildMachines.Windows2022,

                  Steps = new List<GithubTask>
                  {
                      new CheckoutTaskV2
                      {
                          Name = "Check out"
                      },

                      new SetupDotNetTaskV1
                      {
                          Name = "Setup .Net",

                          TargetDotNetVersion = new TargetDotNetVersion
                          {
                              DotNetVersion = "8.0.300",
                              IncludePrerelease = true
                          }
                      },

                      new RestoreTask
                      {
                          Name = "Restore"
                      },

                      new DotNetBuildTask
                      {
                          Name = "Building Project"
                      },

                      new TestTask
                      {
                          Name = "Running Tests"
                      }
                  }
              }
          }
      }
};

string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
string directoryPath = Path.GetDirectoryName(buildScriptPath);

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

aDotNetClient.SerializeAndWriteToFile(githubPipeline, path: buildScriptPath);