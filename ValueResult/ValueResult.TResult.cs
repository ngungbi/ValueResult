namespace Ngb.ValueResult;

public readonly struct ValueResult<TResult> {
    public readonly TResult Value;
    public readonly IError? Error;
    public bool IsSuccess => Error == null;
    public bool IsFailed => Error != null;

    public ValueResult(TResult value) {
        Value = value;
        Error = null;
    }

    public ValueResult(TResult value, IError? error) {
        Value = value;
        Error = error;
    }

    public void Deconstruct(out TResult value, out IError? error) {
        value = Value;
        error = Error;
    }

    public static implicit operator ValueResult<TResult>(TResult value) {
        return new ValueResult<TResult>(value);
    }

    public static implicit operator TResult(ValueResult<TResult> value) {
        return value.Value;
    }

    public static implicit operator ValueResult<TResult>(Error? error) {
        return new ValueResult<TResult>(default!, error);
    }

    public static implicit operator ValueResult<TResult>(Exception exception) {
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
