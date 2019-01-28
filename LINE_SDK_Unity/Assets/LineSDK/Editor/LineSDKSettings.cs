using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

namespace Line.LineSDK.Editor {
    class LineSDKSettings : ScriptableObject {
        public const string assetPath = "Assets/Editor/LineSDK/LineSDKSettings.asset";

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