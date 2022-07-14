namespace Ngb.ValueResult;

public class ErrorException : Exception, IError {
    public ErrorException(string? message = null) : base(message) { }
    public ErrorException(string? message, Exception inner) : base(message, inner) { }
    public int StatusCode { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorDetails { get; init; }
    public bool Is(string errorCode) => ErrorCode == errorCode;
}
