namespace DotNet7RPG.Models;

public class ServiceResponse<T>
{
    public T? Payload { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}