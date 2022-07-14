namespace Ngb.ValueResult;

public static class ErrorExtension {
    public static Error? AsError(this IError error) {
        return error as Error;
    }

    public static Error? AsError<T>(this ValueResult<T> result) {
        return result.Error as Error;
    }

    public static Error? AsError(this ValueResult result) {
        return result.Error as Error;
    }
}
