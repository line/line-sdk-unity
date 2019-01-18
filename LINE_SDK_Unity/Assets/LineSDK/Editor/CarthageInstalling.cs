using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Diagnostics;
using System;

namespace Line.LineSDK {
    public class CarthageInstalling {

        static string projectRoot;

        [PostProcessBuildAttribute(2)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
                return;
            }

            projectRoot = pathToBuiltProject;
        
            AddSearchPaths();
            if (!CheckCarthage()) {
                // Carthage is required for install LINE SDK on iOS.
                return;
            }

            PrepareCartfile();
            CarthageUpdate();

        }

        static bool CheckCarthage() {
            var carthageExisting = Run("which", "carthage");
            if (string.IsNullOrEmpty(carthageExisting)) {
                var text = @"LINE SDK integrating failed. Building LINE SDK for iOS target requires Carthage, but it is not installed. Please run ""brew install carthage"" and try again.";
                UnityEngine.Debug.LogError(text);
                var clicked = EditorUtility.DisplayDialog("Carthage not found", text, "More", "Cancel");
                if (clicked) {
                    Application.OpenURL("https://github.com/Carthage/Carthage");
                }
                return false;
            }
            return true;
        }

        static string CartfilePath { 
            get { return Path.Combine(projectRoot, "Cartfile"); }
        }

        static void PrepareCartfile() {

        }

        static void CarthageUpdate() {
            var currentDirectory = Directory.GetCurrentDirectory();

            var cartFileLocation = CartfilePath;
            if (File.Exists(cartFileLocation)) {
                var text = @"A Cartfile is already existing under Xcode project root. Skipping copying of LINE SDK's Cartfile. Make sure you have setup Podfile correctly if you are using another package also requires CocoaPods.";
                UnityEngine.Debug.Log(text);
            } else {
                var cartfile = Path.Combine(currentDirectory, "Assets/LineSDK/Editor/Cartfile");
                UnityEngine.Debug.Log(cartfile);
                File.Copy(cartfile, cartFileLocation);
            }

            Directory.SetCurrentDirectory(projectRoot);
            var log = Run("carthage", "update --cache-builds");
            UnityEngine.Debug.Log(log);
            Directory.SetCurrentDirectory(currentDirectory);
        }

        static void AddSearchPaths() {
            AddSearchPath("/usr/local/bin/");
        }

        static string AppendingHome(string path) {
            var homePath = System.Environment.GetEnvironmentVariable("HOME");
            return Path.Combine(homePath, path);
        }

        static void AddSearchPath(string path) {
            var name = "PATH";
            var currentPath = System.Environment.GetEnvironmentVariable(name);
            var newPath = currentPath + ":" + path;
            UnityEngine.Debug.Log(newPath);
            var target = EnvironmentVariableTarget.Process;
            System.Environment.SetEnvironmentVariable(name, newPath, target);
        }

        static string Run(string command, string args) {
            ProcessStartInfo psi = new ProcessStartInfo(); 
            psi.FileName = command;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            if (args != null) {
                psi.Arguments = args;
            }
            Process p = Process.Start(psi); 
            string strOutput = p.StandardOutput.ReadToEnd(); 
            p.WaitForExit(); 
            return strOutput;
        }
    }
}