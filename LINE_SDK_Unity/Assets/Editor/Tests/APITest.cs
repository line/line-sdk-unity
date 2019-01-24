using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using System.Linq;

using Line.LineSDK;

public class APITest {
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
        LineAPI.RefreshAccessToken(result => {
            Assert.True(result.IsSuccess);
            result.MatchOk(token => {
                called = true;
                Assert.AreEqual("abc123", token.Value);
                Assert.AreEqual(12345, token.ExpiresIn);
                Assert.AreEqual("abcdefg", token.IdToken);
                Assert.AreEqual("abc321", token.RefreshToken);
                Assert.AreEqual("profile openid", token.Scope);
                Assert.AreEqual("Bearer", token.TokenType);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(LineAPI.actions);
    }

    [Test]
    public void APITestRefreshTokenError() {
        var json = @"
        {
            ""code"": 123,
            ""message"": ""error""
        }
        ";
        var called = false;
        LineAPI.RefreshAccessToken(result => {
            Assert.True(result.IsFailure);
            result.MatchError(error => {
                called = true;
                Assert.AreEqual(123, error.Code);
                Assert.AreEqual("error", error.Message);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiError(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(LineAPI.actions);
    }

    [Test]
    public void APITestRevokeTokenOk() {
        var json = "{}";
        var called = false;
        LineAPI.RevokeAccessToken(result => {
            called = true;
            Assert.True(result.IsSuccess);
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
    }

    [Test]
    public void APITestRevokeTokenError() {
        var json = @"
        {
            ""code"": 123,
            ""message"": ""error""
        }
        ";
        var called = false;
        LineAPI.RevokeAccessToken(result => {
            Assert.True(result.IsFailure);
            result.MatchError(error => {
                called = true;
                Assert.AreEqual(123, error.Code);
                Assert.AreEqual("error", error.Message);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiError(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
    }

    [Test]
    public void APITestVerifyAccessTokenOk() {
        var json = @"
        {
            ""client_id"": ""12345678"",
            ""expires_in"": 12345,
            ""scope"": ""profile openid""
        }
        ";
        var called = false;
        LineAPI.VerifyAccessToken(result => {
            Assert.True(result.IsSuccess);
            result.MatchOk(value => {
                called = true;
                Assert.AreEqual(value.ChannelId, "12345678");
                Assert.AreEqual(12345, value.ExpiresIn);
                Assert.AreEqual(value.Scope, "profile openid");
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(LineAPI.actions);
    }

    [Test]
    public void APITestVerifyAccessTokenError() {
        var json = @"
        {
            ""code"": 123,
            ""message"": ""error""
        }
        ";
        var called = false;
        LineAPI.RefreshAccessToken(result => {
            Assert.True(result.IsFailure);
            result.MatchError(error => {
                called = true;
                Assert.AreEqual(123, error.Code);
                Assert.AreEqual("error", error.Message);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiError(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
    }

    [Test]
    public void APITestGetProfileOk() {
        var json = @"
        {
            ""displayName"": ""testuser"",
            ""userId"": ""user_id"",
            ""pictureUrl"": ""https://example.com/abcd"",
            ""statusMessage"": ""Hi""
        }
        ";
        var called = false;
        LineAPI.GetProfile(result => {
            Assert.True(result.IsSuccess);
            result.MatchOk(profile => {
                called = true;
                Assert.AreEqual("user_id", profile.UserId);
                Assert.AreEqual("testuser", profile.DisplayName);
                Assert.AreEqual("Hi", profile.StatusMessage);
                Assert.AreEqual("https://example.com/abcd", profile.PictureUrl);
                Assert.AreEqual("https://example.com/abcd/large", profile.PictureUrlLarge);
                Assert.AreEqual("https://example.com/abcd/small", profile.PictureUrlSmall);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(LineAPI.actions);
    }

    [Test]
    public void APITestGetProfileError() {
        var json = @"
        {
            ""code"": 123,
            ""message"": ""error""
        }
        ";
        var called = false;
        LineAPI.RefreshAccessToken(result => {
            called = true;
            Assert.True(result.IsFailure);
            result.MatchError(error => {
                Assert.AreEqual(123, error.Code);
                Assert.AreEqual("error", error.Message);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiError(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
    }

    [Test]
    public void APITestGetBotFriendshipStatusOk() {
        var json = @"
        {
            ""friendFlag"": true
        }
        ";
        var called = false;
        LineAPI.GetBotFriendshipStatus(result => {
            Assert.True(result.IsSuccess);
            result.MatchOk(value => {
                called = true;
                Assert.True(value.IsFriend);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(LineAPI.actions);
    }

    [Test]
    public void APITestGetBotFriendshipStatusError() {
        var json = @"
        {
            ""code"": 123,
            ""message"": ""error""
        }
        ";
        var called = false;
        LineAPI.GetBotFriendshipStatus(result => {
            called = true;
            Assert.True(result.IsFailure);
            result.MatchError(error => {
                Assert.AreEqual(123, error.Code);
                Assert.AreEqual("error", error.Message);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiError(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
    }
}