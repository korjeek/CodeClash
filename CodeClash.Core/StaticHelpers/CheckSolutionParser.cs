namespace CodeClash.Core;

public static class CheckSolutionParser
{
    public static bool IsResultOk(string result) => result.Split("\r\n")[0] == "OK";
    public static float GetMeanWorkingTime(string result) => float.Parse(result.Split("\r\n")[1].Split()[1]);
}