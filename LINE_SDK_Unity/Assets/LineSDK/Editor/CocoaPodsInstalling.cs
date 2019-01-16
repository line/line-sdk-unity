using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Diagnostics;
using System;

public class CocoaPodsInstalling {
    [PostProcessBuildAttribute(2)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
        if (target == BuildTarget.iOS) {

            // Add usual ruby runtime manager path to process.
            AddSearchPaths();

            var podExisting = Run("which", "pod");
            UnityEngine.Debug.Log(podExisting);
        }
    }

    static void AddSearchPaths() {
        AddSearchPath("/.rbenv/shims");
        AddSearchPath("/.rvm/scripts/rvm");
    }

    static string AppendingHome(string path) {
        var homePath = System.Environment.GetEnvironmentVariable("HOME");
        return Path.Combine(homePath, path);
    }

    static void AddSearchPath(string path) {
        var name = "PATH";
        var currentPath = System.Environment.GetEnvironmentVariable(name);
        var target = EnvironmentVariableTarget.Process;
        var newPath = currentPath + ":" + AppendingHome(path);
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