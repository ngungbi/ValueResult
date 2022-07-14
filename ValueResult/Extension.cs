namespace Ngb.ValueResult;

public static class ValueResultExtension {
    public static ValueResult AsValueResult<T>(this ValueResult<T> result) {
        return result.IsSuccess ? ValueResult.Success : result.AsError();
    }
}
