using System.Diagnostics;
using NUnit.Engine;
using NUnit;

namespace CodeClash.UserSolutionTest;

public class TimeMemoryListener : ITestEventListener
{
    private readonly Dictionary<string, long> testMemoryUsage = new();
    private readonly Dictionary<string, double> testExecutionTime = new();
    private long memoryBeforeTest;
    private Stopwatch stopwatch;
    
    public void OnTestEvent(string report)
    {
        var xmlNode = XmlHelper.CreateXmlNode(report);

        switch (xmlNode.Name)
        {
            case "start-test":
                GC.Collect();
                memoryBeforeTest = GC.GetTotalMemory(false);

                stopwatch = Stopwatch.StartNew();
                break;

            case "test-case":
                stopwatch.Stop();
                var elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

                var testName = xmlNode.Attributes["fullname"]?.Value;
                var memoryAfterTest = GC.GetTotalMemory(false);
                var memoryUsed = memoryAfterTest - memoryBeforeTest;

                if (!string.IsNullOrEmpty(testName))
                {
                    testMemoryUsage[testName] = memoryUsed;
                    testExecutionTime[testName] = elapsedMilliseconds;
                }
                break;
        }
    }

    public Dictionary<string, long> GetTestMemoryUsage() => testMemoryUsage;
    
    public Dictionary<string, double> GetTestExecutionTime() => testExecutionTime;
}