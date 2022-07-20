namespace Ngb.ValueResult;

public static class ErrorExtension {
    public static Error? AsError(this IError error) {
        return error as Error;
    }

    public static Error? AsError<T>(this Result<T> result) {
        return result.Error as Error;
    }

    public static Error? AsError(this Result result) {
        return result.Error as Error;
    }
}
