using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CsHms.Akshay
{
    public partial class Remittance : Form
    {
        public Remittance()
        {
            InitializeComponent();
            
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogue = new OpenFileDialog();
            openFileDialogue.Filter = "Excel Files|*.xml;";
            if (openFileDialogue.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strFilepath = openFileDialogue.FileName;
                    DataTable dtData = GetDataTableFromXml(strFilepath);
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        dgvData.DataSource = dtData;
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message.ToString()); }
            }


        }
        

public DataTable GetDataTableFromXml(string filePath)
{
    DataTable dt = new DataTable();

    XmlDocument doc = new XmlDocument();
    doc.Load(filePath);

   

    // Dynamically add columns based on the first <Activity> node
    string xmlkey = System.Configuration.ConfigurationSettings.AppSettings.Get("XMLKey");
    XmlNode firstActivityNode = doc.SelectSingleNode("//" + xmlkey + "");
    if (firstActivityNode != null)
    {
        foreach (XmlNode childNode in firstActivityNode.ChildNodes)
        {
            if (!dt.Columns.Contains(childNode.Name))
            {
                dt.Columns.Add(childNode.Name);
            }
        }
    }

    // Populate the DataTable with values from all <Activity> nodes
    XmlNodeList nodeList = doc.SelectNodes("//" + xmlkey + "");
    foreach (XmlNode node in nodeList)
    {
        DataRow dr = dt.NewRow();
        foreach (XmlNode childNode in node.ChildNodes)
        {
            // Check if the column exists to handle cases where <Activity> nodes may have different child nodes
            if (dt.Columns.Contains(childNode.Name))
            {
                dr[childNode.Name] = childNode.InnerText;
            }
            else
            {
                // Optionally handle the scenario where new, unexpected tags are found
                // For example, you could add a new column dynamically (not shown here)
                
            }
        }
        dt.Rows.Add(dr);
    }

    return dt;
}


        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = null;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string sourceFile = System.Configuration.ConfigurationSettings.AppSettings.Get("FliePath");
             if (System.IO.File.Exists(sourceFile))
        {
            // Configure the SaveFileDialog
            saveFileDialog.FileName = System.IO.Path.GetFileName(sourceFile); // Suggest the name of the file to save
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"; // Filter files

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file name and save the file
                string destFile = saveFileDialog.FileName;
                System.IO.File.Copy(sourceFile, destFile, true); // true to overwrite the destination file if it exists
            }
        }
        else
        {
            MessageBox.Show("File not found: "+sourceFile+"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}