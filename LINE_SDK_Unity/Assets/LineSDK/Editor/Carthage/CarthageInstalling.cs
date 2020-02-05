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
    public class CarthageInstalling {

        static string projectRoot;

        [PostProcessBuildAttribute(2)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
                return;
            }

            if (!LineSDKSettings.GetOrCreateSettings().UseCarthage) {
                return;
            }

            projectRoot = pathToBuiltProject;
        
            ShellCommand.AddSearchPath("/usr/local/bin/");
            ShellCommand.AddPossibleRubySearchPaths();

            if (!CheckCarthage()) {
                // Carthage is required for install LINE SDK on iOS.
                return;
            }

            PrepareCartfile();
            CarthageUpdate();
            ConfigureXcodeForCarthage();
            AddCarthageCopyPhase();
        }

        static bool CheckCarthage() {
            var carthageExisting = ShellCommand.Run("which", "carthage");
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
            var file = Cartfile.LoadOrCreate(CartfilePath);
            var predefinedItem = bundledCartfile().items[0];
            file.UpdateOrAddEntry(predefinedItem.source, predefinedItem.content, predefinedItem.version);
            file.Save();
        }

        static string BundledCartfilePath {
            get { 
                var currentDirectory = Directory.GetCurrentDirectory();
                return Path.Combine(currentDirectory, "Assets/LineSDK/Editor/Carthage/Cartfile");
            }
        }

        static Cartfile bundledCartfile() {
            return Cartfile.Load(BundledCartfilePath);
        }

        static void CarthageUpdate() {
            var currentDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(projectRoot);
            var log = ShellCommand.Run("carthage", "update --cache-builds");
            UnityEngine.Debug.Log(log);
            Directory.SetCurrentDirectory(currentDirectory);
        }

        static void ConfigureXcodeForCarthage() {
            var path = PBXProject.GetPBXProjectPath(projectRoot);
            var project = new PBXProject();
            project.ReadFromFile(path);
            var target = project.TargetGuidByName(PBXProject.GetUnityTargetName());

            project.AddFileToBuild(
                target, 
                project.AddFile("Carthage/Build/iOS/LineSDK.framework", "Frameworks/LineSDK.framework", 
                PBXSourceTree.Source));
            project.AddFileToBuild(
                target, 
                project.AddFile("Carthage/Build/iOS/LineSDKObjC.framework", "Frameworks/LineSDKObjC.framework.framework", 
                PBXSourceTree.Source));

            project.SetBuildProperty(target, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            project.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
            project.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Carthage/Build/iOS");

            project.WriteToFile(path);
        }

        static void AddCarthageCopyPhase() {
            var currentDirectory = Directory.GetCurrentDirectory();
            var gemFile = Path.Combine(currentDirectory, "Assets/LineSDK/Editor/Carthage/Gemfile");
            var installScript = Path.Combine(currentDirectory, "Assets/LineSDK/Editor/Carthage/copy_carthage_framework.rb");

            File.Copy(gemFile, Path.Combine(projectRoot, "Gemfile"), true);
            File.Copy(installScript, Path.Combine(projectRoot, "copy_carthage_framework.rb"), true);

            Directory.SetCurrentDirectory(projectRoot);
            ShellCommand.Run("gem", "install bundler --no-document");
            ShellCommand.Run("bundle", "install --path vendor/bundle");
            ShellCommand.Run("bundle", "exec ruby copy_carthage_framework.rb");
            Directory.SetCurrentDirectory(currentDirectory);
        }
    }
}
#endif