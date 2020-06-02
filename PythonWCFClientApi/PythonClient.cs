using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseWebApi;
using Newtonsoft.Json.Linq;
using PythonWCFCommon;
using static PythonWCFCommon.RunPythonCmd;

namespace PythonWCFClientApi
{
    public class PythonClient
    { 

        BaseWeb m_http;

        public PythonClient(string ipAddress , int port)
        {
            m_http = new BaseWeb(ipAddress, port);
        }

        public bool GetPythonStatus(out PYTHON_STATUS pstatus, out string outMessage)
        {
            string status;
            bool b = m_http.GetSync("GetPythonStatus", out status, out outMessage);
            if (b == true)
            {
                outMessage = string.Empty;
                JObject j = JObject.Parse(status);
                pstatus = (PYTHON_STATUS)int.Parse(j["Result"].ToString());
            }
            else
            {
                pstatus =  PYTHON_STATUS.NOT_STARTED;
            }
            return b;
        }
        public bool RunPyhton(bool openPythonConsoleWindow, string pyhonScriptCode, out string outMessage)
        {
             RunPythonCmd pcmd = new RunPythonCmd
             {
                 openPythonConsoleWindow = openPythonConsoleWindow,
                 pyhonScriptCode = pyhonScriptCode
             };
             return m_http.PostSync<RunPythonCmd>("RunPythonClient" , pcmd, out outMessage);
        }

        public bool GetVersion(out string version, out string outMessage)
        {
            bool b = m_http.GetSync("GetVersion", out version, out outMessage);
            if (b == true)
            {
                outMessage = string.Empty;
                JObject j = JObject.Parse(version);
                version = j["Result"].ToString();             
            }
            else
            {                
                version = string.Empty;
            }
            return b;
        }
    }
}
