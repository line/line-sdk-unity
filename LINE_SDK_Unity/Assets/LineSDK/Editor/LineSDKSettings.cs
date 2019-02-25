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
using System.Collections.Generic;
using System;
using System.IO;

namespace Line.LineSDK.Editor {
    class LineSDKSettings : ScriptableObject {
        const string assetPath = "Assets/Editor/LineSDK/LineSDKSettings.asset";

        internal static string[] dependencyManagerOptions = new string[] { "CocoaPods", "Carthage" };

        [SerializeField]
        private string iOSDependencyManager;

        internal static int DependencySelectedIndex(string selected) {
            return Array.IndexOf(dependencyManagerOptions, selected);
        }

        internal bool UseCocoaPods { get { return iOSDependencyManager.Equals("CocoaPods"); } }
        internal bool UseCarthage { get { return iOSDependencyManager.Equals("Carthage"); } }
        
        internal static LineSDKSettings GetOrCreateSettings() {
            var settings = AssetDatabase.LoadAssetAtPath<LineSDKSettings>(assetPath);
            if (settings == null) {
                settings = ScriptableObject.CreateInstance<LineSDKSettings>();
                settings.iOSDependencyManager = "CocoaPods";

                Directory.CreateDirectory("Assets/Editor/LineSDK/");

                AssetDatabase.CreateAsset(settings, assetPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings() {
            return new SerializedObject(GetOrCreateSettings());
        }
    }

    static class LineSDKSettingsProvider {

        static SerializedObject settings;

        #if UNITY_2018_3_OR_NEWER
        private class Provider : SettingsProvider {
            public Provider(string path, SettingsScope scope = SettingsScope.User): base(path, scope) {}
            public override void OnGUI(string searchContext) {
                DrawPref();
            }
        }
        [SettingsProvider]
        static SettingsProvider MyNewPrefCode() {
            return new Provider("Preferences/LINE SDK");
        }
        
        #else
        [PreferenceItem("LINE SDK")]
        #endif
        static void DrawPref() {
            if (settings == null) {
                settings = LineSDKSettings.GetSerializedSettings();
            }
            settings.Update();
            EditorGUI.BeginChangeCheck();

            var property = settings.FindProperty("iOSDependencyManager");
            var selected = LineSDKSettings.DependencySelectedIndex(property.stringValue);

            selected = EditorGUILayout.Popup("iOS Dependency Manager", selected, LineSDKSettings.dependencyManagerOptions);

            if (selected < 0) {
                selected = 0;
            } 
            property.stringValue = LineSDKSettings.dependencyManagerOptions[selected];

            if (EditorGUI.EndChangeCheck()) {
                settings.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }
            
        }
    }
}