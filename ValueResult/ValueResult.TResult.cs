namespace Ngb.ValueResult;

public readonly struct Result<TResult> {
    public readonly TResult Value;
    public readonly IError? Error;
    public bool IsSuccess => Error == null;
    public bool IsFailed => Error != null;

    public Result(TResult value) {
        Value = value;
        Error = null;
    }

    public Result(TResult value, IError? error) {
        Value = value;
        Error = error;
    }

    public void Deconstruct(out TResult value, out IError? error) {
        value = Value;
        error = Error;
    }

    public static implicit operator Result<TResult>(TResult value) {
        return new Result<TResult>(value);
    }

    public static implicit operator TResult(Result<TResult> value) {
        return value.Value;
    }

    public static implicit operator Result<TResult>(Error? error) {
        return new Result<TResult>(default!, error);
    }

    public static implicit operator Result<TResult>(Exception exception) {
        if (exception is IError error) {
            return new Error(error.StatusCode, error.ErrorCode, error.ErrorDetails);
        }

        var message = exception.GetType().FullName;
        var details = exception.Message;
        return new Error(errorCode: message, errorDetails: details) {
            // InnerException = exception
        };
    }
    
}
