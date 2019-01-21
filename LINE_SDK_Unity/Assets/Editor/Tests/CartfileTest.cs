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
