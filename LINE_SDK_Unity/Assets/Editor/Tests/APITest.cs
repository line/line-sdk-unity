using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Linq;

using Line.LineSDK;

public class APITokenTest {
	[Test]
	public void APITestRefreshTokenOk() {
        var json = @"
        {
            ""access_token"": ""abc123"",
            ""expires_in"": 12345,
            ""id_token"": ""abcdefg"",
            ""refresh_token"": ""abc321"",
            ""scope"":""profile openid"",
            ""token_type"": ""Bearer""
        }
        ";
        var called = false;
        API.RefreshAccessToken(result => {
            called = true;
            Assert.True(result.IsSuccess);
            result.MatchOk(token => {
                Assert.AreEqual(token.Value, "abc123");
            });
        });

        var identifier = API.actions.Keys.ToList()[0];
        API._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
	}

    [Test]
	public void APITestRefreshTokenFail() {
        var json = @"
        {
            ""code"": 123,
            ""message"": ""error""
        }
        ";
        var called = false;
        API.RefreshAccessToken(result => {
            called = true;
            Assert.True(result.IsFailure);
            result.MatchError(error => {
                Assert.AreEqual(error.Code, 123);
                Assert.AreEqual(error.Message, "error");
            });
        });

        var identifier = API.actions.Keys.ToList()[0];
        API._OnApiError(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
	}
}