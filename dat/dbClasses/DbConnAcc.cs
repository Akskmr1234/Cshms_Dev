using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

    class DBConnAcc
    {
        private String strDBType;
        private String strConString;
        private String strErrorDesc;
        private static OleDbConnection Conn = null;
        private static OleDbTransaction dbtTran = null;
        public DBConnAcc()
        {
            strConString = ConfigurationSettings.AppSettings.Get("ConString") + ";Jet OLEDB:Database Password=atscigolsoft";            
            strDBType = "A";
            strErrorDesc = "";
            OpenConnection(strConString);
        }
        public DBConnAcc(String ConSring)
        {
            OpenConnection(ConSring);
        }
        public DBConnAcc(String FileNm,bool FileOp)
        {
            if (FileOp)
            {                
                strConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileNm.Replace("\\", "/"); ;                
            }
            else
            {
                strConString = FileNm;
            }
            OpenConnection(strConString);
        }
        public DBConnAcc(OleDbConnection Con)
        {
            Conn = Con;
        }
        public DBConnAcc(Object Con)
        {
            Conn = (OleDbConnection)Con;
        }
        private void OpenConnection(String ConString)
        {
            if (Conn == null)
            {
                Conn = new OleDbConnection(ConString);
                Conn.Open();
            }
            //try { Conn = new OleDbConnection(strConString); }
            //catch { strErrorDesc = "Connection Failed"; }
        }


        public OleDbConnection Connection()
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
        public Int32 ExecuteNonQuery(String Sql)
        {
            OleDbCommand cmd = new OleDbCommand(Sql, Conn);
            return cmd.ExecuteNonQuery();
        }
        public DataTable ExecuteQuery(String Sql)
        {            
            DataTable dt = new DataTable(); 
            OleDbDataAdapter da = new OleDbDataAdapter(Sql, Conn);
            da.Fill(dt);
            return dt;
        }
        public DataTable ExecuteStoredProcedure(String Sql)
        {
            DataTable dt = new DataTable();
            OleDbCommand cm = new OleDbCommand();
            cm.CommandText = Sql;
            cm.Connection = Conn;
            cm.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter da = new OleDbDataAdapter(Sql, Conn);
            OleDbDataReader drd = cm.ExecuteReader();
            dt.Load(drd);
            drd.Dispose();
            return dt;
        }
        public object  ExecuteScalar(String Sql)
        {
            object objRtnValue;
            OleDbCommand cmd = new OleDbCommand(Sql, Conn);
            objRtnValue=cmd.ExecuteScalar();
            return objRtnValue;
        }

        public Object Execute_ReadSP_SingleColRet(String Sql)
        {
            DataTable dt = new DataTable();
            OleDbCommand cm = new OleDbCommand(Sql,Conn);
            cm.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter da = new OleDbDataAdapter(Sql, Conn);
            Object  obj = null;
            OleDbDataReader drd = cm.ExecuteReader();
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
            OleDbDataAdapter da = new OleDbDataAdapter(Sql, Conn);
            
            da.FillSchema(dt,new SchemaType());
            return dt;
        }
        public Int32 UpdateDataTable(String Sql, DataTable DataTbl)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(Sql, Conn);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            return da.Update(DataTbl);            
        }
        public DataTable getBlankDataTbl(String TableName)
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
        //Function AutoNumFix() As Long
        // url : http://www.allenbrowne.com/func-ADOX.html#ResetSeed, http://www.allenbrowne.com/ser-40.html
        //    'Purpose:   Find and optionally fix tables in current project where
        //    '               Autonumber is negative or below actual values.
        //    'Return:    Number of tables where seed was reset.
        //    'Reply to dialog: Yes = change table. No = skip table. Cancel = quit searching.
        //    'Note:    Requires reference to Microsoft ADO Ext. library.
        //    Dim cat As New ADOX.Catalog 'Catalog of current project.
        //    Dim tbl As ADOX.Table       'Each table.
        //    Dim col As ADOX.Column      'Each field
        //    Dim varMaxID As Variant     'Highest existing field value.
        //    Dim lngOldSeed As Long      'Seed found.
        //    Dim lngNewSeed As Long      'Seed after change.
        //    Dim strTable As String      'Name of table.
        //    Dim strMsg As String        'MsgBox message.
        //    Dim lngAnswer As Long       'Response to MsgBox.
        //    Dim lngKt As Long           'Count of changes.

        //    Set cat.ActiveConnection = CurrentProject.Connection
        //    'Loop through all tables.
        //    For Each tbl In cat.Tables
        //    lngAnswer = 0&
        //    If tbl.Type = "TABLE" Then  'Not views.
        //    strTable = tbl.Name     'Not system/temp tables.
        //    If Left(strTable, 4) <> "Msys" And Left(strTable, 1) <> "~" Then
        //    'Find the AutoNumber column.
        //    For Each col In tbl.Columns
        //    If col.Properties("Autoincrement") Then
        //    If col.Type = adInteger Then
        //        'Is seed negative or below existing values?
        //        lngOldSeed = col.Properties("Seed")
        //        varMaxID = DMax("[" & col.Name & "]", "[" & strTable & "]")
        //        If lngOldSeed < 0& Or lngOldSeed <= varMaxID Then
        //            'Offer the next available value above 0.
        //            lngNewSeed = Nz(varMaxID, 0) + 1&
        //            If lngNewSeed < 1& Then
        //                lngNewSeed = 1&
        //            End If
        //            'Get confirmation before changing this table.
        //            strMsg = "Table:" & vbTab & strTable & vbCrLf & _
        //                "Field:" & vbTab & col.Name & vbCrLf & _
        //                "Max:  " & vbTab & varMaxID & vbCrLf & _
        //                "Seed: " & vbTab & col.Properties("Seed") & _
        //                vbCrLf & vbCrLf & "Reset seed to " & lngNewSeed & "?"
        //            lngAnswer = MsgBox(strMsg, vbYesNoCancel + vbQuestion, _
        //                "Alter the AutoNumber for this table?")
        //            If lngAnswer = vbYes Then   'Set the value.
        //                col.Properties("Seed") = lngNewSeed
        //                lngKt = lngKt + 1&
        //                'Write a trail in the Immediate Window.
        //                Debug.Print strTable, col.Name, lngOldSeed, " => " & lngNewSeed
        //            End If
        //        End If
        //    End If
        //    Exit For 'Table can have only one AutoNumber.
        //    End If
        //    Next    'Next column
        //    End If
        //    End If
        //    'If the user chose Cancel, no more tables.
        //    If lngAnswer = vbCancel Then
        //    Exit For
        //    End If
        //    Next    'Next table.

        //    'Clean up
        //    Set col = Nothing
        //    Set tbl = Nothing
        //    Set cat = Nothing
        //    AutoNumFix = lngKt
        //    End Function
    }