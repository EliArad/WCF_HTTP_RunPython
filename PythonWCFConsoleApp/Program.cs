using Microsoft.Win32;
using PythonWCFShared;
using RegistryClassApi;
using System;
 
 
namespace PythonWCFConsoleApp
{
    class Program
    {
      
        static void Main(string[] args)
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

            Console.ReadLine();
            WCFConnect.Close();

        }
         
        
    }
}
