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
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

namespace Line.LineSDK.Editor {
    public class FrameworkAdding {
        
        private const string frameworkPath = "Assets/Plugins/iOS/LineSDK/vendor";
        private const string destinationPath = "Libraries/Plugins/iOS/LineSDK"; 
        private const string frameworkName = "LineSDKObjC.xcframework";
        
        [PostProcessBuild(3)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            if (target != BuildTarget.iOS) {
                return;
            }

            var frameworkSourcePath = Path.Combine(frameworkPath, frameworkName);
            var frameworkDestinationPath = Path.Combine(destinationPath, frameworkName);
            
            // Copy the whole xcframework folder to target Xcode project on disk.
            CopyDirectory(frameworkSourcePath, Path.Combine(pathToBuiltProject,frameworkDestinationPath));

            var projPath = Path.Combine(pathToBuiltProject, "Unity-iPhone.xcodeproj/project.pbxproj");
            PBXProject proj = new PBXProject();
            proj.ReadFromFile(projPath);

            string frameworkTargetGuid = null;
            string appTargetGuid = null;
#if UNITY_2019_3_OR_NEWER
            frameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();
            appTargetGuid = proj.GetUnityMainTargetGuid();
#else
            appTargetGUID = proj.TargetGuidByName("Unity-iPhone");
#endif
            // Add the LINE SDK to the project and get its reference.
            string sdkGuid = proj.AddFile(frameworkDestinationPath, frameworkDestinationPath);
            // Add the reference to app target and embed it.
            proj.AddFileToEmbedFrameworks(appTargetGuid, sdkGuid);
            
            if (frameworkTargetGuid != null) {
                // If the framework target exists, add the reference to the framework target without embedding.
                var buildPhaseGuid = proj.GetFrameworksBuildPhaseByTarget(frameworkTargetGuid);
                proj.AddFileToBuildSection(frameworkTargetGuid, buildPhaseGuid, sdkGuid);
            }

            proj.WriteToFile(projPath);
        }

        private static void CopyDirectory(string sourcePath, string destPath)
        {
            Directory.CreateDirectory(destPath);
            foreach (var file in Directory.GetFiles(sourcePath)) {
                if (file.EndsWith(".meta")) {
                    continue;
                }
                File.Copy(file, Path.Combine(destPath, Path.GetFileName(file)));
            }
            foreach (var dir in Directory.GetDirectories(sourcePath))
                CopyDirectory(dir, Path.Combine(destPath, Path.GetFileName(dir)));
        }
    }
}
#endif