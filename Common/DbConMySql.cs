using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Text;

/// <summary>
/// For Mysql connection
/// </summary>

public class DbConMySql
{
    private String strDBType;
    private String strConString;
    private String strErrorDesc;
    private OdbcConnection Conn;
    private CommFuncs comFuncs = new CommFuncs();
    private static OdbcTransaction dbtTran = null;
   
    public DbConMySql()
    {
        strConString ="";
    }
    public DbConMySql(String ConString)
    {
        strConString = ConString;
        OpenConnection(strConString);
    }
    private void OpenConnection(String ConString)
    {
        strDBType = "M";
        strErrorDesc = "";
        try
        {
            Conn.ConnectionTimeout = 9000;
        }
        catch { }
        if (Conn == null)
            Conn = new OdbcConnection(strConString);
        if (Conn.State != ConnectionState.Open)
            Conn.Open();
    }

    private void OpenConnection()
    {
        try { OpenConnection(""); }
        catch (Exception ex) { strErrorDesc = "Error On Connection Open." + Environment.NewLine + ex.Message; }
    }

    private void CloseConnection()
    {
        try
        {
            strErrorDesc = "";
            Conn.Close();
            Conn = null;
        }
        catch (Exception ex) { strErrorDesc = "Error On Connection Close." + Environment.NewLine + ex.Message; }
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

    public Int32 ExecuteNonQuery(String Sql)
    {
        OpenConnection();
        OdbcCommand cmd = new OdbcCommand(Sql, Conn);
        int intCnt = cmd.ExecuteNonQuery();
        CloseConnection();
        return intCnt;
    }

    public DataTable ExecuteQuery(String Sql)
    {
        strErrorDesc = "";
        OpenConnection();
        try
        {
            DataTable dt = new DataTable();
            OdbcDataAdapter da = new OdbcDataAdapter(Sql, Conn);
            da.Fill(dt);
            CloseConnection();
            return dt;
        }
        catch (Exception ex)
        {
            CloseConnection();
            strErrorDesc = ex.Message;            
            return null;
        }        
    }

    // ***********************************************
}
  
