using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace Line.LineSDK {
    public class CarthageInstalling {

        static string projectRoot;

        [PostProcessBuildAttribute(2)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
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
            // file.UpdateOrAddEntry("github", "\"line/line-sdk-ios-swift\"", "~> 5.1");
            file.UpdateOrAddEntry("github", "\"line/line-sdk-ios-swift\"", "\"feature/convenience-methods\"");
            file.Save();
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
            Debug.Log("Gem path: " + ShellCommand.Run("which", "gem"));
            ShellCommand.Run("gem", "install bundler --no-ri --no-rdoc");
            ShellCommand.Run("bundle", "install --path vendor/bundle");
            ShellCommand.Run("bundle", "exec ruby copy_carthage_framework.rb");
            Directory.SetCurrentDirectory(currentDirectory);
        }
    }
}