using PythonWCFHttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
 
namespace PythonWCFShared
{
    public class WCFConnect
    {
        static ServiceHost host = null;
        public static void Close()
        {
           
            if (host != null)
                host.Close();
        }   
        public static void OpenService(string ipAddress, int port)
        {

            try
            { 
                string url = string.Format("http://{0}:{1}/", ipAddress, port);
                WebServiceHost host = new WebServiceHost(typeof(PythonService), new Uri(url));
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IPythonService), new WebHttpBinding(), "");

                ep.Behaviors.Add(new EnableCorsEndpointBehavior());

                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();
                Console.WriteLine("Open Server At:" + url);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
    }
}
