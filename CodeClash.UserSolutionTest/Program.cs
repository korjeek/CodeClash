using System.Globalization;
using System.Xml;
using NUnit.Engine;

namespace CodeClash.UserSolutionTest;

public class Program
{
	public static void Main(string[] args)
	{
		var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
		var testAssemblies = Directory.GetFiles(currentDirectory, "*CodeClash.UserSolutionTest.dll");
		
		if (testAssemblies.Length == 0)
		{
			Console.WriteLine("Test assembly not found.");
			return;
		}
		
		using var testEngine = TestEngineActivator.CreateInstance();
		var testPackage = new TestPackage(testAssemblies)
		{
			Settings = { ["InternalTraceLevel"] = "Off" }
		};
		var runner = testEngine.GetRunner(testPackage);
		var filter = TestFilter.Empty;
		
		var result = runner.Run(null, filter);
		var amountTestCases = runner.CountTestCases(filter);
		ParseResults(result, amountTestCases);
	}
	
	private static void ParseResults(XmlNode? result, int amountTestCases)
	{
		var totalTime = 0.0f;
		
		foreach (XmlNode test in result?.SelectNodes("//test-case")!)
		{
			var testName = test.Attributes?["name"]?.Value;
			var testResult = test.Attributes?["result"]?.Value;
			
			float.TryParse(
				test.Attributes?["duration"]?.Value, 
				CultureInfo.InvariantCulture, 
				out var duration);
			
			totalTime += duration * 1000;
			
			if (testResult != "Failed") continue;
			var failureMessage = test.SelectSingleNode("failure/message")?.InnerText;
			Console.WriteLine($"{testResult}\n{testName}\n{failureMessage}");
			return;
		}
		Console.WriteLine("OK");
		Console.WriteLine($"Time: {totalTime / amountTestCases:F2} ms.");
	}
}
