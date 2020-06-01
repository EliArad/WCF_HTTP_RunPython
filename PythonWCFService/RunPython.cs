using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PythonWCFHttpService
{
    public class PythonRun
    {
        System.Diagnostics.Process m_proc;
        string m_currentDirectory;
        public delegate void PythonRunCallback(int code);
       
        public bool RunPythonClient(bool openPythonConsoleWindow , 
                                   string pyhonScriptCode,                                 
                                   out string outMessage,
                                   Action<bool> cb)                                    
        {
            outMessage = string.Empty;
            m_currentDirectory = Directory.GetCurrentDirectory();

            System.Diagnostics.ProcessStartInfo StartInfo = new ProcessStartInfo();
            StartInfo.FileName = @"c:\windows\system32\cmd";

            if (openPythonConsoleWindow == true)
            {
                StartInfo.RedirectStandardOutput = false;
                StartInfo.RedirectStandardInput = false;
                StartInfo.CreateNoWindow = false;
                StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            }
            else
            {
                // must read if not make it false
                StartInfo.RedirectStandardOutput = false;
                StartInfo.RedirectStandardInput = false;
                StartInfo.CreateNoWindow = true;
            }

            StartInfo.UseShellExecute = false;
             

         
            if (File.Exists(pyhonScriptCode) == false)
            {
                outMessage = "Python script not found" + pyhonScriptCode;
                return false;
            }
            //Directory.SetCurrentDirectory(@"C:\Python27");
            if (File.Exists(@"C:\Python27\python.exe") == false)
            {                
                outMessage = @"C:\Python27\python.exe not exist";
                return false;
            }

            StartInfo.Arguments = @"/K   C:\Python27\python.exe " + pyhonScriptCode;
            //System.Diagnostics.Process.Start(p.StartInfo.FileName, p.StartInfo.Arguments);

            m_proc = new System.Diagnostics.Process();

            //proc.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            //proc.ErrorDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            m_proc.StartInfo = StartInfo;
            m_proc.Start();

            if (cb != null)
            {
                Task task = new Task(() => { WaitForFinished(cb); });
                task.Start();
            }
            return true;  
        }
        void WaitForFinished(Action<bool> cb)
        {
            Process[] p = null;
            p = Process.GetProcessesByName("python");
            while (p.Length == 0)
            {
                Thread.Sleep(500);
                p = Process.GetProcessesByName("python");
            }
                              
            while (true)
            {                
                try 
                {                
                    p = Process.GetProcessesByName("python");
                    if (p.Length == 0)
                        break;
                    Thread.Sleep(200);
                }
                catch(Exception)
                {
                    m_proc.Kill();
                    cb(false);
                    return;
                }                
            }
            cb(true);
        }
    }
}
