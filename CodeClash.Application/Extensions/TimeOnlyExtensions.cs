using System.Text;

namespace CodeClash.Application.Extensions;

public static class TimeOnlyExtensions
{
    public static string GetTimeStrToSendFront(this TimeOnly timeOnly)
    {
        var result = new StringBuilder();
        if (timeOnly.Hour != 0)
            result.Append($"{timeOnly.Hour}h ");
        if (timeOnly.Minute != 0)
            result.Append($"{timeOnly.Minute}m ");
        if (timeOnly.Second != 0)
            result.Append($"{timeOnly.Second}s");
        return result.ToString();
    }
}