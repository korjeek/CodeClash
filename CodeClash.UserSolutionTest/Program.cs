using System.Diagnostics;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CodeClash.UserSolutionTest;

public class Program
{
	public static void Main(string[] args)
	{
		RunAllTests();
		Console.WriteLine("Test execution completed.");
	}

	private static void RunAllTests()
	{
		var testClass = new SolutionTaskTests();

		RunTest(testClass.FindSumTest_Example1);
		RunTest(testClass.FindSumTest_Example2);
		RunTest(testClass.FindSumTest_Example3);
		RunTest(testClass.OneElementIsZero);
		RunTest(testClass.OneElementIsNegative);
		RunTest(testClass.AllElementsIsNegative);
	}

	private static void RunTest(Action testMethod)
	{
		var stopwatch = Stopwatch.StartNew();
		long initialMemory = GC.GetTotalMemory(true);

		try
		{
			testMethod(); // Запуск теста
			stopwatch.Stop();
			long finalMemory = GC.GetTotalMemory(false); // Память после теста

			// Лог успешного выполнения
			Console.WriteLine($"Test {testMethod.Method.Name} passed in {stopwatch.ElapsedMilliseconds} ms.");
		}
		catch (AssertionException ex)
		{
			stopwatch.Stop();
			long finalMemory = GC.GetTotalMemory(false); // Память после теста

			// Запись данных о неудавшемся тесте в jconfig1.json
			var failureInfo = new
			{
				FailedTest = testMethod.Method.Name,
				ExecutionTimeMs = stopwatch.ElapsedMilliseconds,
				MemoryUsedBytes = finalMemory - initialMemory,
				ErrorMessage = ex.Message
			};

			// Вывод информации в консоль перед записью в файл
			Console.WriteLine("Writing failure info to jconfig1.json...");

			// Запись в файл
			File.WriteAllText("jsconfig1.json", JsonConvert.SerializeObject(failureInfo, Formatting.Indented));

			Console.WriteLine($"Test {testMethod.Method.Name} failed: {ex.Message}");
		}
	}
}
