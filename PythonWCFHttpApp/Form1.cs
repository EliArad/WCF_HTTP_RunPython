using PythonWCFClientApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PythonWCFCommon.RunPythonCmd;

namespace PythonWCFHttpApp
{
    public partial class Form1 : Form
    {
        PythonClient m_client;
        Thread m_thread = null;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            txtIpAddress.Text = Properties.Settings.Default.IpAddress;
            txtPort.Text = Properties.Settings.Default.Port.ToString();
            txtPythonScript.Text = Properties.Settings.Default.PythonScript;
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            if (m_client.GetVersion(out string version, out string outMessage) == false)
            {
                MessageBox.Show(outMessage);
            }
            else
            {
                txtVersion.Text = version;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                m_client = new PythonClient(txtIpAddress.Text, int.Parse(txtPort.Text));
                groupBox1.Enabled = true;

                m_running = true;
                m_thread = new Thread(StatusThread);
                m_thread.Start();                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        bool m_running = false;
        AutoResetEvent m_ev = new AutoResetEvent(false);
        void StatusThread()
        {
            while (m_running)
            {
                ShowStatus();
                m_ev.WaitOne(5000);
            }
        }
        void ShowStatus()
        {
            if (m_client.GetPythonStatus(out PYTHON_STATUS pstatus, out string outMessage) == false)
            {
                lblStatus.Text = "error";
            }
            else
            {
                lblStatus.Text = pstatus.ToString();
            }
        }

        private void btnRunPython_Click(object sender, EventArgs e)
        {
            btnRunPython.ForeColor = Color.Black;            
            if (m_client.RunPyhton(true, txtPythonScript.Text, out string outMessage) == false)
            {
                btnRunPython.ForeColor = Color.Red;
            }
            else
            {
                btnRunPython.ForeColor = Color.Green;
                ShowStatus();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_running = false;
            m_ev.Set();
            Properties.Settings.Default.IpAddress = txtIpAddress.Text;
            Properties.Settings.Default.Port = int.Parse(txtPort.Text);
            Properties.Settings.Default.PythonScript = txtPythonScript.Text;
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_client.KillPythonScript(out int numKilled, out string outMessage) == false)
            {
                MessageBox.Show("Failed to kill");
            }
            else
            {
                ShowStatus();
                MessageBox.Show("Killed: " + numKilled.ToString());
            }
        }
    }
}
