using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;

namespace LabInterface.CareData.LisWebAPI
{
    public class Common
    {
        private string DataSetToJsonObj(DataSet ds)
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


                        if (ds.Tables["Main"].Columns[j].ToString() != "OrderDetails")
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
                            JsonString.Append("\"" + "OrderDetails" + "\":" + DataTableToJsonObj(ds.Tables["SER"]));
                            //JsonString.Append(",");
                        }
                        //else if (ds.Tables["Main"].Columns[j].ToString() == "PaymentDetails")
                        //{
                        //    JsonString.Append("\"" + "PaymentDetails" + "\":" + DataTableToJsonObj(ds.Tables["PAY"]));
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

        private string DataTableToJsonObj(DataTable dt)
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



    }
}
