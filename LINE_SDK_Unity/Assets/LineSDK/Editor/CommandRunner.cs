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

using System.Diagnostics;
using System;
using System.IO;

namespace Line.LineSDK.Editor {
    internal class ShellCommand {
        static string AppendingHome(string path) {
            var homePath = System.Environment.GetEnvironmentVariable("HOME");
            return Path.Combine(homePath, path);
        }

        internal static void AddPossibleRubySearchPaths() {
            AddSearchPath(AppendingHome(".rbenv/shims"));
            AddSearchPath(AppendingHome(".rvm/scripts/rvm"));
            AddSearchPath("/usr/local/bin/");
        }

        internal static void AddSearchPath(string path) {
            var name = "PATH";
            var currentPath = System.Environment.GetEnvironmentVariable(name);
            var newPath = path + ":" + currentPath;
            var target = EnvironmentVariableTarget.Process;
            System.Environment.SetEnvironmentVariable(name, newPath, target);
        }

        internal static string Run(string command, string args) {
            ProcessStartInfo psi = new ProcessStartInfo(); 
            psi.FileName = command;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            if (args != null) {
                psi.Arguments = args;
            }
            psi.RedirectStandardError = true;
            Process p = Process.Start(psi); 
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            if (!string.IsNullOrEmpty(error)) {
                UnityEngine.Debug.LogError(error);
            }
            p.Close();
            return output;
        }
    }
}