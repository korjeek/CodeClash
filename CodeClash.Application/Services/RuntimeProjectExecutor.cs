using System.Diagnostics;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;

namespace CodeClash.Core.Services;

public static class RuntimeProjectExecutor
{
    public static string HandleProject(string projectName, string solutionDirectory)
    {
        var projectPath = Path.Combine(solutionDirectory, projectName, $"{projectName}.csproj");
        var outputDirectory = Path.Combine(solutionDirectory, projectName, "bin", "Debug");
        
        var buildResult = BuildProject(projectPath, outputDirectory);
        if (buildResult.OverallResult != BuildResultCode.Success)
            return buildResult.Exception.ToString();
        
        var executablePath = Path.Combine(outputDirectory, $"{projectName}.exe");
        return RunProjectExecutable(executablePath);
    }
    
    private static BuildResult BuildProject(string projectPath, string outputDirectory)
    {
        var projectCollection = new ProjectCollection();
        var buildParameters = new BuildParameters(projectCollection)
        {
            Loggers = new[] { new ConsoleLogger() }
        };
        var globalProperties = new Dictionary<string, string>
        {
            { "Configuration", "Debug" },
            { "OutputPath", outputDirectory }
        };
        
        var buildRequest = new BuildRequestData(
            projectPath,
            globalProperties,
            null,
            ["Build"],
            null);
        
        return BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);
    }
    
    private static string RunProjectExecutable(string executablePath)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processStartInfo);
        var output = process?.StandardOutput.ReadToEnd();
        process?.WaitForExit();
        return output;
    }
}