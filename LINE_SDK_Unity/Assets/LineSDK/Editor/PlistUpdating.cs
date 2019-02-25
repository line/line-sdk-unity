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
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace Line.LineSDK.Editor {
    public class PlistUpdating {
        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
                return;
            }

            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
        
            PlistElementDict rootDict = plist.root;

            SetupURLScheme(rootDict);
            SetupQueriesSchemes(rootDict);

            File.WriteAllText(plistPath, plist.WriteToString());
        }

        static void SetupURLScheme(PlistElementDict rootDict) {
            PlistElementArray array = GetOrCreateArray(rootDict, "CFBundleURLTypes");
            var lineURLScheme = array.AddDict();
            lineURLScheme.SetString("CFBundleTypeRole", "Editor");
            lineURLScheme.SetString("CFBundleURLName", "LINE SDK");
            var schemes = lineURLScheme.CreateArray("CFBundleURLSchemes");
            schemes.AddString("line3rdp." + rootDict["CFBundleIdentifier"].AsString());
        }

        static void SetupQueriesSchemes(PlistElementDict rootDict) {
            PlistElementArray array = GetOrCreateArray(rootDict, "LSApplicationQueriesSchemes");
            array.AddString("lineauth2");
        }

        static PlistElementArray GetOrCreateArray(PlistElementDict dict, string key) {
            PlistElement array = dict[key];
            if (array != null) {
                return array.AsArray();
            } else {
                return dict.CreateArray(key);
            }
        }
    }
}
#endif