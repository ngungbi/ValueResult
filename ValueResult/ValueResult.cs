namespace Ngb.ValueResult;

public readonly struct ValueResult {
    public readonly IError? Error;
    public bool IsSuccess => Error == null;
    public bool IsFailed => Error != null;

    public ValueResult(IError? error) {
        Error = error;
    }

    public ValueResult() {
        Error = null;
    }

    public static implicit operator ValueResult(Error? error) {
        return new ValueResult(error);
    }

    public static implicit operator ValueResult(Exception exception) {
        if (exception is IError error) {
            return new Error(error.StatusCode, error.ErrorCode, error.ErrorDetails);
        }

        var message = exception.GetType().FullName;
        var details = exception.Message;
        return new Error(errorCode: message, errorDetails: details) {
            // InnerException = exception
        };
        // return new Result<T>(default!, error);
    }

    public static ValueResult Success => new();
}
