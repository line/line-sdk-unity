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


#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;

using Line.LineSDK.Editor;

public class CartfileTest {

    [Test]
    public void CartfileParsing() {
        var p = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Editor/Tests/Fixture/SampleCartfile");
        var text = File.ReadAllText(p);
        var file = new Cartfile(text);
        Assert.AreEqual(file.items.Count, 35); 
        Assert.AreEqual(file.Output, text + "\n");
    }

    [Test]
    public void CartfileItemParse() {
        var text = @"github ""line/line-sdk-ios-swift"" ""feature/convenience-methods""";
        var item = new Cartfile.Item(text);
        Assert.AreEqual("github", item.source);
        Assert.AreEqual("\"line/line-sdk-ios-swift\"", item.content);
        Assert.AreEqual("\"feature/convenience-methods\"", item.version);

        text = @"github ""line/line-sdk-ios-swift"" ~> 5.1";
        item = new Cartfile.Item(text);
        Assert.AreEqual("github", item.source);
        Assert.AreEqual("\"line/line-sdk-ios-swift\"", item.content);
        Assert.AreEqual("~> 5.1", item.version);

        text = @"github ""line/line-sdk-ios-swift"" == 5.1.1";
        item = new Cartfile.Item(text);
        Assert.AreEqual("github", item.source);
        Assert.AreEqual("\"line/line-sdk-ios-swift\"", item.content);
        Assert.AreEqual("== 5.1.1", item.version);
    }

    public void CartfileItemUpdate() {
        var text = @"github ""line/line-sdk-ios-swift"" ~> 5.1";
        var item = new Cartfile.Item(text);
        bool result = item.UpdateOrAddEntry("github", "abc", "== 1.0.0");
        Assert.False(result);

        item = new Cartfile.Item(text);
        result = item.UpdateOrAddEntry("github", "\"line/line-sdk-ios-swift\"", "~> 5.1");
        Assert.True(result);
        Assert.False(item.dirty);

        item = new Cartfile.Item(text);
        result = item.UpdateOrAddEntry("github", "\"line/line-sdk-ios-swift\"", "~> 5.2");
        Assert.True(result);

        Assert.AreEqual("github", item.source);
        Assert.AreEqual("\"line/line-sdk-ios-swift\"", item.content);
        Assert.AreEqual("~> 5.2", item.version);
        Assert.True(item.dirty);
    }
}
#endif