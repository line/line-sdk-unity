using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

using Line.LineSDK;

public class FlattenActionTest {

    [Serializable]
    class Foo {
        public string userId;        
    }

	[Test]
	public void FlattenActionTestCallOk() {
        var called = false;
		var action = FlattenAction.JsonFlatten<Foo>(result => {
            result.MatchOk(value => {
                called = true;
                Assert.AreEqual(value.userId, "123");
            });
        });
        action.CallOk(@"{""userId"": ""123""}");
        Assert.True(called);
	}

    [Test]
	public void FlattenActionTestCallError() {
        var called = false;
		var action = FlattenAction.JsonFlatten<Foo>(result => {
            result.MatchError(value => {
                called = true;
                Assert.AreEqual(value.code, 123);
                Assert.AreEqual(value.message, "test");
            });
        });
        action.CallError(@"{""code"": 123, ""message"":""test""}");
        Assert.True(called);
	}
}
