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
using System.Linq;

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
                ""pictureUrl"": ""https://example.com/abcd"",
                ""statusMessage"": ""Hi""
            },
            ""friendshipStatusChanged"": true
        }
        ";
        var called = false;
        
        LineAPI.Login(new string[] {"profile"}, null, result => {
            result.MatchOk(value => {
                called = true;
                Assert.AreEqual("abc123", value.AccessToken.Value);
            });
        });

        var identifier = LineAPI.actions.Keys.ToList()[0];
        LineAPI._OnApiOk(CallbackPayload.WrapValue(identifier, json));
        Assert.True(called);
        Assert.IsEmpty(LineAPI.actions);
    }

    
}