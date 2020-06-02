using Microsoft.Win32;
using PythonWCFShared;
using RegistryClassApi;
using System;
 
using System.ServiceProcess;
 

namespace PythonWCFHttpWinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                string ipAddress = "";
                const string ServerRegistry = "SOFTWARE\\Goji Solutions\\Harmoni";
                clsRegistry reg = new clsRegistry();
                ipAddress = reg.GetStringValue(Registry.LocalMachine, ServerRegistry, "LocalIpAddress");
                if (ipAddress == null)
                {
                    Console.WriteLine("Failed to get the resgitry key for local ip address");
                }
                else
                {
                    WCFConnect.OpenService(ipAddress, 8022);
                } 
                            
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                Console.ReadKey();
                return;
            }
        }

        protected override void OnStop()
        {
            WCFConnect.Close();
        }
    }
}
