using Microsoft.AspNetCore.Mvc;

namespace Ngb.ValueResult.Extension;

public static class ValueResultMvcExtension {
    public static IActionResult ToActionResult(this ValueResult result, int statusCode = 204) {
        if (result.IsSuccess) {
            return new StatusCodeResult(statusCode);
        }

        return new ObjectResult(result.Error) {
            StatusCode = result.Error!.StatusCode
        };
    }
    
    public static IActionResult ToActionResult<T>(this ValueResult<T> result) {
        if (result.IsSuccess) {
            return new OkObjectResult(result.Value);
        }

        return new ObjectResult(result.Error) {
            StatusCode = result.Error!.StatusCode
        };
    }
    
}
