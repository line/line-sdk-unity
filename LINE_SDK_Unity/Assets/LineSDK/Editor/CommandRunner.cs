using System.Diagnostics;
using System;
using System.IO;

namespace Line.LineSDK {
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
            p.WaitForExit();
            UnityEngine.Debug.Log(output);
            UnityEngine.Debug.Log(p.StandardError.ReadToEnd());
            p.Close();
            return output;
        }
    }
}