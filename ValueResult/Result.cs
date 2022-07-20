namespace Ngb.ValueResult;

public readonly struct Result {
    public readonly IError? Error;
    public bool IsSuccess => Error == null;
    public bool IsFailed => Error != null;

    public Result(IError? error) {
        Error = error;
    }

    public Result() {
        Error = null;
    }

    public static implicit operator Result(Error? error) {
        return new Result(error);
    }

    public static implicit operator Result(Exception exception) {
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

    public static Result Success => new();
}
