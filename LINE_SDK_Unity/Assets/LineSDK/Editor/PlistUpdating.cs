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