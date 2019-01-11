using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

using Line.LineSDK;

public class AccessTokenTest {

	[Test]
	public void AccessTokenTestParse() {
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
        var accessToken = JsonUtility.FromJson<AccessToken>(json);
        Assert.NotNull(accessToken);
        Assert.AreEqual(accessToken.Value, "abc123");
        Assert.AreEqual(accessToken.ExpiresIn, 12345);
        Assert.AreEqual(accessToken.IdToken, "abcdefg");
        Assert.AreEqual(accessToken.RefreshToken, "abc321");
        Assert.AreEqual(accessToken.Scope, "profile openid");
        Assert.AreEqual(accessToken.TokenType, "Bearer");

	}
}
