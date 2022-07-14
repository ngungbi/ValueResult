namespace Ngb.ValueResult;

public interface IError {
    int StatusCode { get; }
    string? ErrorCode { get; }
    string? ErrorDetails { get; }
    bool Is(string statusCode);
}

public class Error : IError {
    public Error() { }

    public Error(int statusCode = 500, string? errorCode = null, string? errorDetails = null) {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        ErrorDetails = errorDetails;
    }

    public int StatusCode { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorDetails { get; init; }

    public bool Is(string errorCode) => ErrorCode == errorCode;
}
