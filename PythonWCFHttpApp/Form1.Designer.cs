namespace PythonWCFHttpApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnRunPython = new System.Windows.Forms.Button();
            this.txtPythonScript = new System.Windows.Forms.TextBox();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(69, 45);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(92, 20);
            this.txtIpAddress.TabIndex = 0;
            this.txtIpAddress.Text = "192.168.21.31";
            this.txtIpAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(186, 45);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(92, 20);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "8022";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRunPython
            // 
            this.btnRunPython.Location = new System.Drawing.Point(454, 108);
            this.btnRunPython.Name = "btnRunPython";
            this.btnRunPython.Size = new System.Drawing.Size(109, 23);
            this.btnRunPython.TabIndex = 2;
            this.btnRunPython.Text = "Run Python Script";
            this.btnRunPython.UseVisualStyleBackColor = true;
            this.btnRunPython.Click += new System.EventHandler(this.btnRunPython_Click);
            // 
            // txtPythonScript
            // 
            this.txtPythonScript.Location = new System.Drawing.Point(10, 108);
            this.txtPythonScript.Name = "txtPythonScript";
            this.txtPythonScript.Size = new System.Drawing.Size(438, 20);
            this.txtPythonScript.TabIndex = 3;
            // 
            // btnGetVersion
            // 
            this.btnGetVersion.Location = new System.Drawing.Point(11, 28);
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.Size = new System.Drawing.Size(75, 23);
            this.btnGetVersion.TabIndex = 4;
            this.btnGetVersion.Text = "Get Version";
            this.btnGetVersion.UseVisualStyleBackColor = true;
            this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(92, 30);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(100, 20);
            this.txtVersion.TabIndex = 5;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(295, 45);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 6;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtVersion);
            this.groupBox1.Controls.Add(this.btnRunPython);
            this.groupBox1.Controls.Add(this.txtPythonScript);
            this.groupBox1.Controls.Add(this.btnGetVersion);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(69, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 146);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(495, 89);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "---";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(454, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Status:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 250);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIpAddress);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnRunPython;
        private System.Windows.Forms.TextBox txtPythonScript;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
    }
}

