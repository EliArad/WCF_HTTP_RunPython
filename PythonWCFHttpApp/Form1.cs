using PythonWCFClientApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PythonWCFCommon.RunPythonCmd;

namespace PythonWCFHttpApp
{
    public partial class Form1 : Form
    {
        PythonClient m_client;
        public Form1()
        {
            InitializeComponent();

            txtIpAddress.Text = Properties.Settings.Default.IpAddress;
            txtPort.Text = Properties.Settings.Default.Port.ToString();
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

                if (m_client.GetPythonStatus(out PYTHON_STATUS pstatus, out string outMessage) == false)
                {
                    lblStatus.Text = pstatus.ToString();
                }
                else
                {
                    lblStatus.Text = "error";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.IpAddress = txtIpAddress.Text;
            Properties.Settings.Default.Port = int.Parse(txtPort.Text);
            Properties.Settings.Default.Save();
        }
    }
}
