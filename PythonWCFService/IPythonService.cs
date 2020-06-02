using PythonWCFCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PythonWCFHttpService
{
    [ServiceContract]
    public interface IPythonService
    {
         
        [OperationContract]
        [WebGet(), CorsEnabled]
        Stream KillPythonScript();
         


        [OperationContract]
        [WebGet(), CorsEnabled]
        Stream GetVersion();

        [OperationContract]
        [WebGet(), CorsEnabled]
        Stream GetPythonStatus();

        [OperationContract]
        [WebGet(), CorsEnabled]
        Stream Login(string username, string password);

         

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "RunPythonClient"), CorsEnabled]
        Stream RunPythonClient(RunPythonCmd pcmd);
         

    } 
}
