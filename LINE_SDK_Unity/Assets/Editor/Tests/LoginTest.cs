using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

using Line.LineSDK;

public class LoginTest {
	[Test]
	public void LoginTestOk() {
        var json = @"
        {
            ""accessToken"": {
                ""access_token"": ""abc123"",
                ""expires_in"": 12345,
                ""id_token"": ""abcdefg"",
                ""refresh_token"": ""abc321"",
                ""scope"":""profile openid"",
                ""token_type"": ""Bearer""
            },
            ""scope"": ""profile openid"",
            ""userProfile"": {
                ""displayName"": ""testuser"",
                ""userId"": ""user_id"",
                ""pictureUrl"": ""https://example.com/image.jpg"",
                ""statusMessage"": ""Hi""
            },
            ""friendshipStatusChanged"": true
        }
        ";
        var called = false;
        
        API.Login(new List<string>{"profile"}, null, result => {
            result.MatchOk(value => {
                called = true;
                Assert.AreEqual("abc123", value.AccessToken.Value);
            });
        });

        var identifier = API.actions.Keys.ToList()[0];
        API._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(API.actions);
	}

    
}