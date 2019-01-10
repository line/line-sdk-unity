using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using Line.LineSDK;

public class ResultTest {

	[Test]
	public void ResultTestCreateWithValue() {
		var result = Result<int>.Ok(1);
        Assert.IsTrue(result.IsSuccess);
        var okCalled = false;
        var errorCalled = false;
        result.Match(
            value => {
                okCalled = true;
                Assert.AreEqual(value, 1);
            },
            error => {
                errorCalled = true;
            }
        );
        Assert.True(okCalled);
        Assert.False(errorCalled);
	}

    [Test]
    public void ResultTestCreateWithError() {
        var error = new Error();
        error.code = 100;
        var result = Result<int>.Error(error);
        Assert.IsTrue(result.IsFailure);
        var okCalled = false;
        var errorCalled = false;
        result.Match(
            value => {
                okCalled = true;
            },
            e => {
                errorCalled = true;
                Assert.AreEqual(e.code, 100);
            }
        );
        Assert.False(okCalled);
        Assert.True(errorCalled);
    }
}
