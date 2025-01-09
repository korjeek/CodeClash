using System.Diagnostics;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;

namespace CodeClash.Application.Services;

public static class RuntimeProjectExecutor
{
    public static string HandleProject(string projectName, string solutionDirectory)
    {
        var projectPath = Path.Combine (solutionDirectory, projectName, $"{projectName}.csproj");
        var outputDirectory = Path.Combine(solutionDirectory, projectName, "bin", "Debug");
        
        var (buildResult, buildErrors) = BuildProject(projectPath, outputDirectory);
        if (buildResult.OverallResult != BuildResultCode.Success)
            return GetErrorsFromLogs(buildErrors);
        
        var executablePath = Path.Combine(outputDirectory, $"{projectName}.exe");
        return RunProjectExecutable(executablePath);
    }
    
    private static (BuildResult, string) BuildProject(string projectPath, string outputDirectory)
    {
        var projectCollection = new ProjectCollection();
        var logWriter = new StringWriter();
        var consoleLogger = new ConsoleLogger(LoggerVerbosity.Minimal, logWriter.Write, null, null);
        
        var buildParameters = new BuildParameters(projectCollection)
        {
            Loggers = new[] { consoleLogger }
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

        var buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);
        
        return (buildResult, logWriter.ToString());
    }

    private static string GetErrorsFromLogs(string logs)
    {
        var logLines = logs.Split(['\\', '\r', '\n']);
        
        var errorLines = logLines
            .Where(line => line.Contains("error CS"))
            .Select(line => line.Replace(@"\", ""))
            .ToList();
        
        return string.Join(Environment.NewLine, errorLines);
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