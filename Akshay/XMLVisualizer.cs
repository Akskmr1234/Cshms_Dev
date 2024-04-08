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
    public partial class XMLVisualizer : Form
    {
        public XMLVisualizer()
        {
            InitializeComponent();
            //Fillnodes("");
        }
        XmlDocument doc = new XmlDocument();
        private void Fillnodes(string filePath)
        {
            try
            {
                //filePath = System.Configuration.ConfigurationSettings.AppSettings.Get("XMLFilepath");
                doc.Load(filePath);
                // Use a List to store tag names
                List<string> tagNames = new List<string>();                   
                GetAllTagNames(doc.DocumentElement, tagNames);
                // Fill the ComboBox with the tag names
                foreach (string tagName in tagNames)
                {
                    cbxNodes.Items.Add(tagName);
                }
                cbxNodes.Items.RemoveAt(0);
                cbxNodes.Items.RemoveAt(1);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        private void GetAllTagNames(XmlNode node, List<string> tagNames)
        {
            
            if (node == null) return;

            // Add the current node's name if it's an element and not already in the list
            if (node.NodeType == XmlNodeType.Element && !tagNames.Contains(node.ParentNode.Name) && node.Attributes != null && node.ParentNode != null)
            {                
                    tagNames.Add(node.ParentNode.Name);
                    
                  
            }
            if (node.HasChildNodes )
            {
                // Recursively search for all child nodes
                foreach (XmlNode child in node.ChildNodes)
                {
                    //if (child.Attributes != null && child.Attributes.Count > 0)
                    GetAllTagNames(child, tagNames);
                }
            }
        }

        private void cbxNodes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvData.DataSource=GetDataTableFromXml(cbxNodes.SelectedItem.ToString());
        }
        public DataTable GetDataTableFromXml(string xmlkey)
        {
            DataTable dt = new DataTable();       


            // Dynamically add columns based on the first <Activity> node
            
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

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogue = new OpenFileDialog();
            openFileDialogue.Filter = "Excel Files|*.xml;";
            if (openFileDialogue.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strFilepath = openFileDialogue.FileName;
                    Fillnodes(strFilepath);                    
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message.ToString()); }
            }
        }      
    }
}