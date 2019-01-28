using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

using Line.LineSDK;

public class LoginResultTest {

    [Test]
    public void LoginResultTestParse() {
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
                ""pictureUrl"": ""https://example.com/abcd"",
                ""statusMessage"": ""Hi""
            },
            ""friendshipStatusChanged"": true
        }
        ";
        var result = JsonUtility.FromJson<LoginResult>(json);
        Assert.NotNull(result);
        
        Assert.NotNull(result.AccessToken);
        Assert.AreEqual("abc123", result.AccessToken.Value);

        var scopes = new string[] {"profile", "openid"};
        Assert.AreEqual(scopes, result.Scopes);

        Assert.NotNull(result.UserProfile);
        Assert.AreEqual("user_id", result.UserProfile.UserId);

        Assert.True(result.IsFriendshipStatusChanged);
    }
}
