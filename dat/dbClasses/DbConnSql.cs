using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
class DbConnSql
{
    private String strDBType;
    private String strConString;
    private String strErrorDesc;
    private static SqlConnection Conn = null;
    private static SqlTransaction dbtTran = null;

    public DbConnSql()
    {
        strConString = System.Configuration.ConfigurationSettings.AppSettings.Get("SqlConString");
        strDBType = "S";
        strErrorDesc = "";
        OpenConnection(strConString);
    }
    public DbConnSql(String ConSring)
    {
        try
        {
            OpenConnection(ConSring);
        }
        catch { }
    }
    public string OrderbyStringField(string strFldName, int intFldLenght)
    {
        return " right('" + new string('0', intFldLenght) + "'+ LTRIM(rtrim(" + strFldName + ")), " + intFldLenght.ToString() + ")";
    }
    public DbConnSql(String FileNm, bool FileOp)
    {
        if (FileOp)
        {
            strConString = "Data  Source=NODE2;Initial Catalog=SmartBiz;Integrated Security=True";
        }
        else
        {
            strConString = FileNm;
        }
        OpenConnection(strConString);
    }
    public DbConnSql(SqlConnection Con)
    {
        Conn = Con;
    }
    public DbConnSql(Object Con)
    {
        Conn = (SqlConnection)Con;
    }
    private void OpenConnection(String ConString)
    {
        try
        {
           
            if (Conn == null)
            {
                Conn = new SqlConnection(ConString);
                Conn.Open();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
    }

    public SqlConnection Connection()
    {
        return Conn;
    }

    public Object ConnectionAsObject()
    {
        return (Object)Conn;
    }
    public String DBType
    {
        get { return strDBType; }
        set { strDBType = value; }
    }
    public String ConString
    {
        get { return strConString; }
        set { strConString = value; }
    }
    public String ErrorDescription
    {
        get { return strErrorDesc; }
        set { strErrorDesc = value; }
    }
    public Int32 ExecuteNonQuery(String Sql,bool trn)
    {
        SqlCommand cmd = new SqlCommand(Sql, Conn);

        return cmd.ExecuteNonQuery();
    }
    public Int32 ExecuteNonQuery(String Sql)
    {
        SqlCommand cmd = new SqlCommand(Sql, Conn);
        cmd.CommandTimeout = 100000;
        return cmd.ExecuteNonQuery();
    }
    public DataTable ExecuteQuery(String Sql,bool trn)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);

        da.Fill(dt);
        return dt;
    }
    public DataTable ExecuteQuery(String Sql)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);      
        da.Fill(dt);
        return dt;
    }
    public Int32 ExecuteNonQuery_OnTran(String Sql)
    {
        SqlCommand cmd = new SqlCommand(Sql, Conn, dbtTran);

        return cmd.ExecuteNonQuery();
    }
    public DataTable ExecuteQuery_OnTran(String Sql)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(Sql, Conn, dbtTran);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dt);
        return dt;
    }
    public DataTable ExecuteStoredProcedure(String Sql)
    {
        DataTable dt = new DataTable();
        SqlCommand cm = new SqlCommand();
        cm.CommandText = Sql;
        cm.Connection = Conn;
        cm.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);
        SqlDataReader drd = cm.ExecuteReader();
        dt.Load(drd);
        drd.Dispose();
        return dt;
    }
    public object ExecuteScalar(String Sql, bool _IsTran)
    {
        object objRtnValue;
        SqlCommand cmd = new SqlCommand(Sql, Conn);
        objRtnValue = cmd.ExecuteScalar();
        return objRtnValue;
    }
    public object ExecuteScalar(String Sql)
    {
        object objRtnValue;
        SqlCommand cmd = new SqlCommand(Sql, Conn);
        objRtnValue = cmd.ExecuteScalar();
        return objRtnValue;
    }
    public Object Execute_ReadSP_SingleColRet(String Sql)
    {
        DataTable dt = new DataTable();
        SqlCommand cm = new SqlCommand(Sql, Conn);
        cm.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);
        Object obj = null;
        SqlDataReader drd = cm.ExecuteReader();
        if (drd.Read())
            obj = drd[0];
        else
            obj = 0;
        drd.Dispose();
        return obj;
    }
    public DataTable ExecuteQuery_Schema(String Sql)
    {
        DataTable dt = new DataTable(); ;
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);

        da.FillSchema(dt, new SchemaType());
        return dt;
    }
    public Int32 UpdateDataTable(String Sql, DataTable DataTbl)
    {
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);
        return da.Update(DataTbl);
    }
    public Int32 UpdateDataTable(String Sql, DataTable DataTbl,bool gg)
    {
        SqlDataAdapter da = new SqlDataAdapter(Sql, Conn);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);
        return da.Update(DataTbl);
    }
    public Int32 UpdateDataTable_OnTran(String Sql, DataTable DataTbl)
    {
        SqlCommand cmd = new SqlCommand(Sql, Conn, dbtTran);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        SqlCommandBuilder cb = new SqlCommandBuilder(da);
        return da.Update(DataTbl);
    }
    public DataTable getBlankDataTbl(String TableName)
    {
        String strSql = "select * from " + TableName + " where 1=2";
        return this.ExecuteQuery(strSql);
    }
    public DataTable getBlankDataTbl(String TableName,bool boole)
    {
        String strSql = "select * from " + TableName + " where 1=2";
        return this.ExecuteQuery(strSql);
    }
    public void BeginTrans()
    {
        if (dbtTran == null)
            dbtTran = Conn.BeginTransaction();
        else
        {
            throw new Exception("Close the running transaction before this operation.");
        }
    }
    public void CommitTrans()
    {
        if (dbtTran == null)
            throw new Exception("No active transactions for commit.");
        else
        {
            dbtTran.Commit();
            dbtTran = null;
        }
    }
    public void RollbackTrans()
    {
        if (dbtTran == null)
            throw new Exception("No active transactions for rollback.");
        else
        {
            dbtTran.Rollback();
            dbtTran = null;
        }
    }
    public void RollbackTrans(bool Silent)
    {
        if (Silent)
        {
            try
            {
                dbtTran.Rollback();
                dbtTran = null;
            }
            catch { }
        }
        else
            RollbackTrans();
    }
    public void InsertDataTableWithSql(DataTable dt, String vPrimaryKey, String vFilterCond,bool boolData)
    {
        int intCols = 0;
        String strIns = "insert into " + dt.TableName + " (";
        String strRet = "";
        CommFuncs mComFuncs = new CommFuncs();
        foreach (DataColumn column in dt.Columns)
        {
            if (vPrimaryKey != column.ColumnName)
                strIns += column.ColumnName + ",";
        }
        strIns = strIns.Length > 0 ? strIns.Substring(0, strIns.Length - 1) : strIns;
        strIns += ") values (";

        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
        {
            intCols = 0;
            strRet = "";
            foreach (DataColumn column in dt.Columns)
            {
                if (vPrimaryKey != column.ColumnName)
                {
                    if (dt.Rows[intRow][intCols] == null || dt.Rows[intRow][intCols] == DBNull.Value)
                    {
                        strRet += "null, ";
                    }
                    else if (dt.Columns[intCols].DataType.Name.ToLower().Contains("int") ||
                            dt.Columns[intCols].DataType.Name.ToLower().Contains("float") ||
                            dt.Columns[intCols].DataType.Name.ToLower().Contains("double") ||
                            dt.Columns[intCols].DataType.Name.ToLower().Contains("decimal"))
                    {
                        if (dt.Rows[intRow][intCols].ToString().Trim() == "")
                            strRet += "0, ";
                        else
                            strRet += dt.Rows[intRow][intCols].ToString() + ", ";
                    }
                    else if (dt.Columns[intCols].DataType.Name.ToLower().Contains("datetime"))
                        strRet += mComFuncs.FormatDBServDate(dt.Rows[intRow][intCols].ToString()) + ", ";
                    else
                        strRet += "'" + dt.Rows[intRow][intCols].ToString() + "', ";
                }
                intCols++;
            }
            strRet = strRet.Length > 1 ? strRet.Substring(0, strRet.Length - 2) : strRet;
            strRet += ")";
            ExecuteNonQuery(strIns + strRet);
        }
    }
    public void InsertDataTableWithSql(DataTable dt, String vPrimaryKey, String vFilterCond)
    {
        int intCols = 0;
        String strIns = "insert into " + dt.TableName + " (";
        String strRet = "";
        CommFuncs mComFuncs = new CommFuncs();
        foreach (DataColumn column in dt.Columns)
        {
            if (vPrimaryKey != column.ColumnName)
                strIns += column.ColumnName + ",";
        }
        strIns = strIns.Length > 0 ? strIns.Substring(0, strIns.Length - 1) : strIns;
        strIns += ") values (";

        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
        {
            intCols = 0;
            strRet = "";
            foreach (DataColumn column in dt.Columns)
            {
                if (vPrimaryKey != column.ColumnName)
                {
                    if (dt.Rows[intRow][intCols] == null || dt.Rows[intRow][intCols] == DBNull.Value)
                    {
                        strRet += "null, ";
                    }
                    else if (dt.Columns[intCols].DataType.Name.ToLower().Contains("int") ||
                            dt.Columns[intCols].DataType.Name.ToLower().Contains("float") ||
                            dt.Columns[intCols].DataType.Name.ToLower().Contains("double") ||
                            dt.Columns[intCols].DataType.Name.ToLower().Contains("decimal"))
                    {
                        if (dt.Rows[intRow][intCols].ToString().Trim() == "")
                            strRet += "0, ";
                        else
                            strRet += dt.Rows[intRow][intCols].ToString() + ", ";
                    }
                    else if (dt.Columns[intCols].DataType.Name.ToLower().Contains("datetime"))
                        strRet += mComFuncs.FormatDBServDate(dt.Rows[intRow][intCols].ToString()) + ", ";
                    else
                        strRet += "'" + dt.Rows[intRow][intCols].ToString() + "', ";
                }
                intCols++;
            }
            strRet = strRet.Length > 1 ? strRet.Substring(0, strRet.Length - 2) : strRet;
            strRet += ")";
            ExecuteNonQuery(strIns + strRet);
        }
    }

    public String UpdateSql(DataTable dt, int vRowIndex, String vFilterCond)
    {
        int intCols = 0;
        String strRet = "update " + dt.TableName + " set ";

        foreach (DataColumn column in dt.Columns)
        {
            strRet += column.ColumnName + "='" + dt.Rows[vRowIndex][intCols].ToString() + "', ";
            intCols++;
        }
        if (strRet.Length > 2) strRet = strRet.Substring(0, strRet.Length - 2);
        vFilterCond = vFilterCond.Trim();
        if (vFilterCond.Length > 0)
            strRet += " where " + vFilterCond;
        return strRet;
    }

    public Int64 UpdateDataTableWithSql(DataTable vDataTable, int vRowIndex, String vFilterCondition)
    {
        return ExecuteNonQuery(UpdateSql(vDataTable, vRowIndex, vFilterCondition));
    }


    
}