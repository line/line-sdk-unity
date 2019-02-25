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
using System.Diagnostics;
using System;

namespace Line.LineSDK.Editor {
    public class CocoaPodsInstalling {
        [PostProcessBuildAttribute(3)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
                return;
            }

            if (!LineSDKSettings.GetOrCreateSettings().UseCocoaPods) {
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
                var podfilePath = Path.Combine(currentDirectory, "Assets/LineSDK/Editor/CocoaPods/Podfile");
                UnityEngine.Debug.Log(podfilePath);
                File.Copy(podfilePath, podFileLocation);
            }

            Directory.SetCurrentDirectory(pathToBuiltProject);
            var log = Run("pod", "install");
            UnityEngine.Debug.Log(log);
            Directory.SetCurrentDirectory(currentDirectory);

            ConfigureXcodeForCocoaPods(pathToBuiltProject);
        }

        static void ConfigureXcodeForCocoaPods(string projectRoot) {
            var path = PBXProject.GetPBXProjectPath(projectRoot);
            var project = new PBXProject();
            project.ReadFromFile(path);
            var target = project.TargetGuidByName(PBXProject.GetUnityTargetName());

            project.SetBuildProperty(target, "GCC_PREPROCESSOR_DEFINITIONS", "$(inherited) LINESDK_COCOAPODS=1");

            project.WriteToFile(path);
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
#endif