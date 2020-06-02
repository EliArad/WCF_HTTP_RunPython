using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PythonWCFCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using static PythonWCFCommon.RunPythonCmd;

namespace PythonWCFHttpService
{

     [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
     ConcurrencyMode = ConcurrencyMode.Multiple,
     Name = "PythonService", Namespace = "PythonWCFHttpService"
     )]
    public class PythonService : IPythonService
    {
  
        public PythonService()
        {            
            
             
        }
        static Object m_lock = new Object();

           
        Stream PrepareResponse(JObject jsonObject)
        {
            var s = JsonSerializer.Create();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                s.Serialize(sw, jsonObject);
            }
              
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        Stream PrepareResponse(int num)
        {
            var s = JsonSerializer.Create();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                s.Serialize(sw, num);
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        Stream PrepareResponseOk()
        {
            JObject jsonObject = new JObject();
            jsonObject["Result"] = "Ok";

            var s = JsonSerializer.Create();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                s.Serialize(sw, jsonObject);
            }
             
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        Stream PrepareResponseMsg(bool msg, bool ok = false)
        {
            JObject jsonObject = new JObject();
            jsonObject["Result"] = msg;

            var s = JsonSerializer.Create();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                s.Serialize(sw, jsonObject);
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            if (ok == true)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        Stream PrepareResponseMsg(string msg, bool ok = false)
        {
            JObject jsonObject = new JObject();
            jsonObject["Result"] = msg;

            var s = JsonSerializer.Create();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                s.Serialize(sw, jsonObject);
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            if (ok == true)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        Stream PrepareResponseMsg(int msg, bool ok = false)
        {
            JObject jsonObject = new JObject();
            jsonObject["Result"] = msg;

            var s = JsonSerializer.Create();
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                s.Serialize(sw, jsonObject);
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            if (ok == true)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }


        public Stream GetVersion()
        {
            return PrepareResponseMsg("Hagai is the king!", true);
        }

        public Stream Login(string email, string password)
        {

            try
            {
                 
                return PrepareResponseMsg("e", true);
            }
            catch (Exception err)
            {
                return PrepareResponseMsg(err.Message);
            }
        }        
         
        public Stream KillPythonScript()
        {
            m_pythonRunner.KillPython(out int numKilled);
            return PrepareResponse(numKilled);            
        }

        PYTHON_STATUS m_pythonStatus = PYTHON_STATUS.NOT_STARTED;

        public Stream GetPythonStatus()
        {           
            return PrepareResponseMsg((int)m_pythonStatus, true);
        }

        PythonRun m_pythonRunner = new PythonRun();
        public Stream RunPythonClient(RunPythonCmd pcmd)
        {

            m_pythonStatus = PYTHON_STATUS.STARTED;
            Console.WriteLine("RunPythonClient {0}  {1}", pcmd.openPythonConsoleWindow, pcmd.pyhonScriptCode);
            m_pythonRunner.RunPythonClient(pcmd.openPythonConsoleWindow, pcmd.pyhonScriptCode, out string outMessage, (cb)=>
            {
                if (cb == false)
                    m_pythonStatus = PYTHON_STATUS.STOP_WITH_FAILURE;
                else
                    m_pythonStatus = PYTHON_STATUS.STOP_OK;
            });
            return PrepareResponseOk();   
        }
    }
}
