using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CsHms.Akshay
{
    public partial class AppUpdator : Form
    {
        public AppUpdator()
        {
            InitializeComponent();
            
        }
        private void ExecuteGitCommand()
        {
            try
            {
                // Disable the button while the command is executing
                btnUpdate.Enabled = false;

                // Clear the output textbox
                txtOutput.Text = "";
                string ParentFolder = Directory.GetParent(Application.StartupPath).FullName;
                // Start a new process to execute Git commands
                Process gitProcess = new Process();
                gitProcess.StartInfo.FileName = "git";
                gitProcess.StartInfo.WorkingDirectory = ParentFolder;
                gitProcess.StartInfo.Arguments = "fetch";
                gitProcess.StartInfo.UseShellExecute = false;
                gitProcess.StartInfo.RedirectStandardOutput = true;
                gitProcess.StartInfo.RedirectStandardError = true;
                gitProcess.OutputDataReceived += GitOutputHandler;
                gitProcess.ErrorDataReceived += GitOutputHandler;
                gitProcess.Start();
                gitProcess.BeginOutputReadLine();
                gitProcess.BeginErrorReadLine();
                gitProcess.WaitForExit();

                // Pull changes after fetching
                gitProcess.StartInfo.Arguments = "pull";
                gitProcess.Start();
                gitProcess.BeginOutputReadLine();
                gitProcess.BeginErrorReadLine();
                gitProcess.WaitForExit();

                // Re-enable the button after command execution
                btnUpdate.Enabled = true;
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand();
            MessageBox.Show("Updated");
        }

        
        // Method to handle Git command output
        private void GitOutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                txtOutput.AppendText(e.Data + Environment.NewLine);

            }
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}