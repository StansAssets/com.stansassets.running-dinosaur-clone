using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace SA.Foundation.Git
{
    public class GitCommands
    {
        static Process CreateGitProcess()
        {
            try
            {
                var result = new Process();
                result.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                result.StartInfo.CreateNoWindow = false;
                result.StartInfo.UseShellExecute = false;
                result.StartInfo.RedirectStandardInput = true;
                result.StartInfo.RedirectStandardOutput = true;
                result.StartInfo.FileName = "cmd.exe";
                result.EnableRaisingEvents = true;
                return result;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                throw e;
            }
        }

        static void RunProcess()
        {
            var process = CreateGitProcess();
            process.Start();
            process.WaitForExit();
        }

        [MenuItem("Test/Git")]
        public static void OpenGitWindow()
        {
            var process = CreateGitProcess();
            process.Start();
            process.StandardInput.WriteLine("cd c:\\Projects");
            process.StandardInput.WriteLine("dir");
        }
    }
}
