using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
namespace CsHms.Akshay.Class
{
    class JsonConvertCls
    {
        public string ConvertDataTableToJson(DataTable dataTable)
        {
            dataTable.Columns.RemoveAt(5);
            dataTable.Columns.RemoveAt(4);
            dataTable.Columns.RemoveAt(2);
            dataTable.Columns.RemoveAt(0);

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[\n");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                jsonBuilder.Append("  {\n");

                
                    string columnName = dataTable.Rows[i][0].ToString();
                    string value = dataTable.Rows[i][1].ToString();

                    jsonBuilder.AppendFormat("    \"{0}\": \"{1}\"", columnName, value);

                    jsonBuilder.Append("  \n}");

                if (i < dataTable.Rows.Count - 1)
                {
                    jsonBuilder.Append(",\n");
                }
                else
                {
                    jsonBuilder.Append("\n");
                }
            }

            jsonBuilder.Append("]");

            string jsonString = jsonBuilder.ToString();

            // Output the JSON string or use it as needed
            return jsonString;
        }






























        public string ConvertDataTabletoString(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);

        }

        public string DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }



        public string DataSetToJsonObj(DataSet ds)
        {
            //DataSet ds = new DataSet();
            //ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables["Main"].Rows.Count > 0)
            {
                //JsonString.Append("[");
                for (int i = 0; i < ds.Tables["Main"].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables["Main"].Columns.Count; j++)
                    {


                        if (ds.Tables["Main"].Columns[j].ToString() != "OrderDetails" && ds.Tables["Main"].Columns[j].ToString() != "PaymentDetails")
                        {
                            if (j < ds.Tables["Main"].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + ds.Tables["Main"].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables["Main"].Rows[i][j].ToString() + "\",");
                            }
                            else if (j == ds.Tables["Main"].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + ds.Tables["Main"].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables["Main"].Rows[i][j].ToString() + "\"");
                            }
                        }
                        else if (ds.Tables["Main"].Columns[j].ToString() == "OrderDetails")
                        {
                            JsonString.Append("ServiceDetails:");
                            JsonString.Append(DataTableToJsonObj(ds.Tables["SER"]));
                          //  JsonString.Append("},");
                        }
                        //else if (ds.Tables["Main"].Columns[j].ToString() == "PaymentDetails")
                        //{
                        //    JsonString.Append("PaymentDetails:");
                        //    JsonString.Append(DataTableToJsonObj(ds.Tables["PAY"]));
                        //}

                    }
                    if (i == ds.Tables["Main"].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                // JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }


    }
}
