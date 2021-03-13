#tool "nuget:?package=Cake.CoreCLR&version=0.36.0"


string BranchName = Argument("branchName", string.Empty);
string CIPlatform = Argument("ciPlatform", string.Empty);

string MasterCIPlatform = "github";
string SelectedEnvironment = string.Empty;

string SolutionName = "ReadingIsGood";
var ProjectsToBePacked  = new Project[]
{
};

var ProjectsToBePublished  = new Project[]
{
  new Project("RIG.WebApi")
};

var TestProjectPatterns = new string[]{
  "./**/FunctionalTest*.csproj",
  "./**/IntegrationTest*.csproj",
  "./**/UnitTest*.csproj",
};

var BuildConfig = "Release";
var DotNetPublishPath = "./dotnet-published/";

string MasterEnvironment = "prod";
var BranchEnvironmentPairs = new Dictionary<string, string>()
{
  {"master", MasterEnvironment },
  {"dev", "develop" },
  {"develop", "develop" }
};

string[] DirectoriesToBeRemoved  = new string[]{
  $"./**/{SolutionName}*/**/bin/**",
  $"./**/{SolutionName}*/**/obj/**",
  $"./**/{SolutionName}*/**/build/**",
  DotNetPublishPath,
};

string CheckEnvVariableStage = "Check Env Variable";
string RemoveDirectoriesStage = "Remove Directories";
string DotNetCleanStage = "DotNet Clean";
string TestStage = "Tests";
string DotNetPublishStage = "DotNet Publish";
string FinalStage = "Final";

Task(CheckEnvVariableStage)
.Does(()=>
{
  if(string.IsNullOrEmpty(BranchName))
    throw new Exception("branchName should not be empty");

  string originalBranchName = BranchName;
  BranchName = BranchName.Replace("refs/heads/","");

  Console.WriteLine($"BranchName = {BranchName} | OriginalBranchName = {originalBranchName}");
  
  if(BranchEnvironmentPairs.ContainsKey(BranchName))
  {
    SelectedEnvironment = BranchEnvironmentPairs[BranchName];
    Console.WriteLine("Selected Env = " + SelectedEnvironment);
  }
  else
  {
    Console.WriteLine("There is no predefined env for this branch");
  }

  Console.WriteLine($"CI Platform = {CIPlatform}");
});

Task(RemoveDirectoriesStage)
.DoesForEach(DirectoriesToBeRemoved  , (directoryPath)=>
{
  var directories = GetDirectories(directoryPath);
    
  foreach (var directory in directories)
  {
    if(!DirectoryExists(directory)) continue;
    
    Console.WriteLine("Directory is cleaning : " + directory.ToString());     

    var settings = new DeleteDirectorySettings
    {
      Force = true,
      Recursive  = true
    };
    DeleteDirectory(directory, settings);
  }
});

Task(DotNetCleanStage)
.IsDependentOn(CheckEnvVariableStage)
.IsDependentOn(RemoveDirectoriesStage)
.Does(()=>
{
  DotNetCoreClean($"{SolutionName}.sln");
});

Task(TestStage)
.IsDependentOn(DotNetCleanStage)
.DoesForEach(TestProjectPatterns, (testProjectPattern)=>
{
  FilePathCollection testProjects = GetFiles(testProjectPattern);
  foreach (var testProject in testProjects)
  {
    Console.WriteLine($"Tests are running : {testProject.ToString()}" );
    var testSettings = new DotNetCoreTestSettings{Configuration = BuildConfig};
    DotNetCoreTest(testProject.FullPath, testSettings);
  }
});

Task(DotNetPublishStage)
// .WithCriteria(() => !string.IsNullOrEmpty(SelectedEnvironment) )
.IsDependentOn(TestStage)
.DoesForEach(ProjectsToBePublished , (project)=>
{
  FilePath projFile = GetCsProjFile(project.Name);
  
  string versionSuffix = SelectedEnvironment;
  if(SelectedEnvironment == MasterEnvironment)
    versionSuffix = string.Empty;

  var settings = new DotNetCorePublishSettings
  {
    Configuration = BuildConfig,
    OutputDirectory = DotNetPublishPath,
    VersionSuffix = versionSuffix
  };

  DotNetCorePublish(projFile.ToString(), settings);
});


Task(FinalStage)
.IsDependentOn(DotNetPublishStage)
.Does(() =>
{
  Console.WriteLine("Operation is completed successfully");
});

var target = Argument("target", FinalStage);
RunTarget(target);

// Utility

FilePath GetCsProjFile(string projectName)
{
  FilePathCollection projFiles = GetFiles($"./**/{projectName}.csproj");
  if(projFiles.Count != 1)
  {
    foreach(var pName in projFiles)
    {
      Console.WriteLine(pName);
    }
    
    throw new Exception($"Only one {projectName}.csproj should be found");
  }
  
  return projFiles.First();
}

class Project
{
  public Project(string name, string runtime = "")
  {
    Name = name;
    Runtime = runtime;
  }
  
  public string Name {get;}
  public string Runtime {get;}
}
