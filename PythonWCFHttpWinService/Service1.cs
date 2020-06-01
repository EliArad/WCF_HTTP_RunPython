using PythonWCFShared;
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
                WCFConnect.OpenService("localhost" , 8020);                
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
