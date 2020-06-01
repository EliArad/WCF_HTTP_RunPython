using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonWCFCommon
{
    public struct RunPythonCmd
    {
        public bool openPythonConsoleWindow;
        public string pyhonScriptCode;

        public enum PYTHON_STATUS
        {
            NOT_STARTED,
            STARTED,
            STOP_WITH_FAILURE,
            STOP_OK
        }
    }
}
