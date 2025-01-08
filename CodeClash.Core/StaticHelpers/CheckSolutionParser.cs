namespace CodeClash.Core;

public static class CheckSolutionParser
{
    public static bool IsResultOk(string result) => result.Split('\n')[0] == "OK";
    public static float GetMeanWorkingTime(string result) => float.Parse(result.Split('\n')[0].Split()[1]);
}