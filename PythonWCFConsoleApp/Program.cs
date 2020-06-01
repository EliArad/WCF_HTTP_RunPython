using PythonWCFShared;
using System;
 
 
namespace PythonWCFConsoleApp
{
    class Program
    {
      
        static void Main(string[] args)
        {

            try
            {
                WCFConnect.OpenService("localhost" , 8022);                
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
