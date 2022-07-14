using Microsoft.AspNetCore.Mvc;
using Ngb.ValueResult;
using Ngb.ValueResult.Extension;
using NUnit.Framework;

namespace Tests;

public class Tests {
    [SetUp]
    public void Setup() { }

    private void AssertSuccess<T>(ValueResult<T> result) {
        AssertSuccess(result.AsValueResult());
    }

    private void AssertSuccess(ValueResult result) {
        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(result.IsFailed);
    }

    private void AssertOkObjectResult(IActionResult result) {
        Assert.IsInstanceOf<OkObjectResult>(result);
        var statusCode = ((OkObjectResult) result).StatusCode;
        Assert.AreEqual(200, statusCode);
    }

    private void AssertFailed<T>(ValueResult<T> result) {
        AssertFailed(result.AsValueResult());
    }

    private void AssertFailed(ValueResult result) {
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.IsFailed);
    }

    private void Assert500Error(IActionResult result) {
        Assert.IsInstanceOf<ObjectResult>(result);
        var statusCode = ((ObjectResult) result).StatusCode;
        Assert.AreEqual(500, statusCode);
    }

    [Test]
    public void DeconstructionClass() {
        var (result, error) = GetClass();
        Assert.IsNull(error);
        Assert.IsInstanceOf<Model>(result);
    }
    
    [Test]
    public void DeconstructionValue() {
        (int result, var error) = GetInt();
        Assert.IsNull(error);
        Assert.AreEqual(result, 123);
    }

    [Test]
    public void ResultWithClass() {
        var result = GetClass();
        AssertSuccess(result);
        var response = result.ToActionResult();
        AssertOkObjectResult(response);
        Assert.Pass();
    }

    [Test]
    public void ResultWithClassError() {
        var result = GetClassError();
        AssertFailed(result);
        var response = result.ToActionResult();
        Assert500Error(response);
    }

    [Test]
    public void ResultWithInt() {
        var result = GetInt();
        var response = result.ToActionResult();
        Assert.IsInstanceOf<ObjectResult>(response);
    }

    [Test]
    public void ResultWithIntError() {
        var result = GetIntError();
        AssertFailed(result);
        var response = result.ToActionResult();
        Assert500Error(response);
    }

    [Test]
    public void ResultWithVoid() {
        var result = GetVoid();
        var response = result.ToActionResult();
        Assert.IsInstanceOf<StatusCodeResult>(response);
        var statusCode = ((StatusCodeResult) response).StatusCode;
        Assert.AreEqual(204, statusCode);
    }

    [Test]
    public void ResultWithVoidError() {
        var result = GetVoidError();
        AssertFailed(result);
        var response = result.ToActionResult();
        Assert500Error(response);
    }

    [Test]
    public void PassedValue() {
        var result = PassInt();
        AssertSuccess(result);
        var response = result.ToActionResult();
        Assert.IsInstanceOf<OkObjectResult>(response);
    }

    [Test]
    public void PassedValueError() {
        var result = PassErrorInt();
        AssertFailed(result);
        var response = result.ToActionResult();
        Assert500Error(response);
    }

    [Test]
    public void PassedVoid() {
        var result = PassVoid();
        AssertSuccess(result);
        var response = result.ToActionResult();
        Assert.IsInstanceOf<StatusCodeResult>(response);
        var statusCode = ((StatusCodeResult) response).StatusCode;
        Assert.AreEqual(204, statusCode);
    }

    [Test]
    public void PassedVoidError() {
        var result = PassErrorVoid();
        AssertFailed(result);
        var response = result.ToActionResult();
        Assert500Error(response);
    }

    private ValueResult<int> PassErrorInt() {
        var result = GetIntError();
        if (result.IsFailed) result.AsError();
        return result;
    }

    private ValueResult PassErrorVoid() {
        var result = GetVoidError();
        if (result.IsFailed) return result.AsError();
        return result;
    }

    private ValueResult<int> PassInt() {
        var result = GetInt();
        if (result.IsFailed) result.AsError();
        return result;
    }

    private ValueResult PassVoid() {
        var result = GetVoid();
        if (result.IsFailed) return result.AsError();
        return result;
    }

    private ValueResult<int> GetInt() {
        return 123;
    }

    private ValueResult<int> GetIntError() {
        return new Error(500, "Test", "Unknown Error");
    }

    private ValueResult<Model> GetClass() {
        return new Model {
            ModelId = 1,
            Description = "Test"
        };
    }

    private ValueResult<Model> GetClassError() {
        return new Error(500, "Test", "Unknown Error");
    }

    private ValueResult GetVoid() {
        return ValueResult.Success;
    }

    private ValueResult GetVoidError() {
        return new Error(500, "Test", "Unknown Error");
    }
}
