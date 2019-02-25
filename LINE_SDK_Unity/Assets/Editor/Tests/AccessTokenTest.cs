//  Copyright (c) 2019-present, LINE Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LINE Corporation.
//
//  As with any software that integrates with the LINE Corporation platform, your use of this software
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
        Assert.AreEqual(accessToken.IdTokenRaw, "abcdefg");
        Assert.AreEqual(accessToken.RefreshToken, "abc321");
        Assert.AreEqual(accessToken.Scope, "profile openid");
        Assert.AreEqual(accessToken.TokenType, "Bearer");
    }

    [Test]
    public void AccessTokenTestParseInvalid() {
        string json1 = null;
        var a1 = JsonUtility.FromJson<AccessToken>(json1);
        Assert.Null(a1);

        var json2 = "";
        var a2 = JsonUtility.FromJson<AccessToken>(json2);
        Assert.Null(a2);

        var json3 = "abc";
        Assert.Throws<ArgumentException>(() => { 
            JsonUtility.FromJson<AccessToken>(json3);
        });

        var json4 = "{}";
        var a4 = JsonUtility.FromJson<AccessToken>(json4);
        Assert.NotNull(a4);
        Assert.Null(a4.Value);
    }
}
