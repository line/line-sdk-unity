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

public class FlattenActionTest {

    [Serializable]
    public class Foo {
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
                Assert.AreEqual(value.Code, 123);
                Assert.AreEqual(value.Message, "test");
            });
        });
        action.CallError(@"{""code"": 123, ""message"":""test""}");
        Assert.True(called);
    }
}
