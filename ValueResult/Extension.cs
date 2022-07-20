namespace Ngb.ValueResult;

public static class ValueResultExtension {
    public static Result AsValueResult<T>(this Result<T> result) {
        return result.IsSuccess ? Result.Success : result.AsError();
    }
}
