using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Diagnostics;
using System;

namespace Line.LineSDK {
    public class CocoaPodsInstalling {
        [PostProcessBuildAttribute(2)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
                return;
            }
        
            // Add usual ruby runtime manager path to process.
            AddSearchPaths();

            var podExisting = Run("which", "pod");
            if (string.IsNullOrEmpty(podExisting)) {
                var text = @"LINE SDK integrating failed. Building LINE SDK for iOS target requires CocoaPods, but it is not installed. Please run ""sudo gem install cocoapods"" and try again.";
                UnityEngine.Debug.LogError(text);
                var clicked = EditorUtility.DisplayDialog("CocoaPods not found", text, "More", "Cancel");
                if (clicked) {
                    Application.OpenURL("https://cocoapods.org");
                }
            }

            var currentDirectory = Directory.GetCurrentDirectory();

            var podFileLocation = Path.Combine(pathToBuiltProject, "Podfile");
            if (File.Exists(podFileLocation)) {
                var text = @"A Podfile is already existing under Xcode project root. Skipping copying of LINE SDK's Podfile. Make sure you have setup Podfile correctly if you are using another package also requires CocoaPods.";
                UnityEngine.Debug.Log(text);
            } else {
                var podfilePath = Path.Combine(currentDirectory, "Assets/LineSDK/Editor/Podfile");
                UnityEngine.Debug.Log(podfilePath);
                File.Copy(podfilePath, podFileLocation);
            }

            Directory.SetCurrentDirectory(pathToBuiltProject);
            var log = Run("pod", "install");
            UnityEngine.Debug.Log(log);
            Directory.SetCurrentDirectory(currentDirectory);
        }

        static void AddSearchPaths() {
            AddSearchPath(AppendingHome(".rbenv/shims"));
            AddSearchPath(AppendingHome(".rvm/scripts/rvm"));
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