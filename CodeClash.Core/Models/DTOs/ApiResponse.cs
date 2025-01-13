namespace CodeClash.API;

public class ApiResponse<T>(bool success, T? data, string? error)
{
    public bool Success { get; set; } = success;
    public T? Data { get; set; } = data;
    public string? Error { get; set; } = error;
}