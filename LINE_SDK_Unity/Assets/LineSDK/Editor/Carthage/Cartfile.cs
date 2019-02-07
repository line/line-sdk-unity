using System.IO;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Line.LineSDK.Editor {
    class Cartfile {

        internal class Item {
            internal string source;
            internal string content;
            internal string version;
            internal bool dirty;
            internal string original;

            internal Item(string line) {
                original = line;
                var trimmed = line.Trim();
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("(git|github|binary)\\s+(\".+\")\\s+(((>=|==|~>)\\s[\\d.]+)|(\".+\"))", options);
                var m = regex.Match(trimmed);
                this.source = m.Groups["1"].ToString();
                this.content = m.Groups["2"].ToString();
                this.version = m.Groups["3"].ToString();
            }

            internal Item(string source, string content, string version) {
                this.source = source;
                this.content = content;
                this.version = version;
                this.dirty = true;
            }

            // Return `true` if the target entry was found and updated. Otherwise, `false`.
            internal bool UpdateOrAddEntry(string source, string content, string version) {
                if (source == this.source && content == this.content) {
                    if (version != this.version) {
                        this.version = version;
                        this.dirty = true;
                    }
                    return true;
                }
                return false;
            }

            internal string GetLine() {
                if (dirty) {
                    return String.Format("{0} {1} {2}", source, content, version);
                } else {
                    return original;
                }
            }
        }

        string path;
        internal List<Item> items = new List<Item>();

        internal static Cartfile LoadOrCreate(string path) {
            Cartfile file;
            if (File.Exists(path)) {
                file = Load(path);
            } else {
                file = new Cartfile();
            }

            file.path = path;
            return file;
        }

        internal static Cartfile Load(string path) {
            var text = File.ReadAllText(path);
            return new Cartfile(text);
        }

        internal Cartfile(string text) {
            var lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines) {
                var item = new Item(line);
                items.Add(item);
            }
        }

        internal void UpdateOrAddEntry(string source, string content, string version) {
            var found = false;
            foreach (var item in items) {
                found = found || item.UpdateOrAddEntry(source, content, version);
            }
            if (!found) {
                var item = new Item(source, content, version);
                items.Add(item);
            }
        }

        Cartfile() { }

        internal void Save() {
            File.WriteAllText(path, Output);
        }

        internal string Output {
            get {
                string result = "";
                foreach (var item in items) {
                    result = result + item.GetLine() + "\n";
                }
                return result;
            }
        }
    }
}