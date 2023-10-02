//  Copyright (c) 2019-present, LY Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LY Corporation.
//
//  As with any software that integrates with the LY Corporation platform, your use of this software
//  is subject to the LINE Developers Agreement [http://terms2.line.me/LINE_Developers_Agreement].
//  This copyright notice shall be included in all copies or substantial portions of the software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

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
                Assert.AreEqual("abcdefg", token.IdTokenRaw);
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