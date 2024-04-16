using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CsHms.Akshay
{
    public partial class AppUpdator : Form
    {
        public AppUpdator()
        {
            InitializeComponent();
            ExecuteGitCommand("fetch");
        }
        private void ExecuteGitCommand(string command)
        {
            try
            {
                ProcessStartInfo StartInfo = new ProcessStartInfo();
                StartInfo.FileName = "cmd.exe";
                StartInfo.Arguments = "/c " + command;
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                StartInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = StartInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                txtOutput.Text = output;
                process.WaitForExit();
                MessageBox.Show("Updated");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand("pull");
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}