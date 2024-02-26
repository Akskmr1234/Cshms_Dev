using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

class CommFuncs
{
    Global mGlobal = new Global();
    DataTable mdtGenSett = null;
    public DataTable mdtColor;
    public void CheckValidNumberKey(KeyPressEventArgs e)
    {
        if ((Keys)e.KeyChar == Keys.Back) return;
        if (!char.IsNumber(e.KeyChar))
            e.Handled = true;
    }
    public void CheckValidDecimalKey(KeyPressEventArgs e, string strValue)
    {
        if ((Keys)e.KeyChar == Keys.Back) return;

        if (e.KeyChar == '.')
        {
            if (strValue.Contains("."))
                e.Handled = true;
            else
                e.Handled = false;
        }
        else
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
    }
    public String DataTableToString(ref DataTable _dt, String _RowSeperator, String _ColSeperator, bool _IsDistinct)
    {
        string strRet = "";
        try
        {
            for (int intRow = 0; intRow < _dt.Rows.Count; intRow++)
            {
                for (int intCol = 0; intCol < _dt.Columns.Count; intCol++)
                {
                    if (_IsDistinct)
                    {

                        if (!strRet.ToUpper().Contains(ConvertToString(_dt.Rows[intRow][intCol]).ToUpper() + _ColSeperator))
                            strRet += ConvertToString(_dt.Rows[intRow][intCol]) + _ColSeperator;
                    }
                    else
                        strRet += ConvertToString(_dt.Rows[intRow][intCol]) + _ColSeperator;
                }
                if (strRet.Length > 0) strRet = strRet.Substring(0, strRet.Length - 1) + _RowSeperator;
            }
            if (strRet.Length > 0) strRet = strRet.Substring(0, strRet.Length - 1);
        }
        catch (Exception ex) { }
        return strRet;
    }
    public String DataTableToString(ref DataTable _dt, String _RowSeperator, String _ColSeperator)
    {
        return DataTableToString(ref _dt, _RowSeperator, _ColSeperator, false);
    }
    public int ConvertToInt(Object obj)
    {
        int RetVal = 0;       
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                RetVal = int.Parse(obj.ToString());
        }
        catch (Exception ex) { }
        return RetVal;
    }
    public bool  checkIsValidNumber(Object obj)
    {
        
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                  int.Parse(obj.ToString());
            return true;
        }
        catch (Exception ex) { }
        return false;
    }


    public Int64 ConvertToInt64(Object obj)
    {
        Int64 RetVal = 0;
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                RetVal = Int64.Parse(obj.ToString());
        }
        catch { }
        return RetVal;
    }
    public long ConvertToLong(Object obj)
    {
        long RetVal = 0;
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                RetVal = int.Parse(obj.ToString());
        }
        catch { }
        return RetVal;
    }
    public Object ConvertToNumberObj(Object obj)
    {
        Object RetVal = 0;
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                RetVal = Decimal.Parse(obj.ToString());
        }
        catch { }
        return RetVal;
    }
    public static string ConvertToHtmlFile(DataTable sentDataTable, string strTitle)
    {

        // Check if the Sent DataTable is not empty or a Null
        if (sentDataTable == null)
        {
            throw new System.ArgumentNullException("sentDataTable");
        }

        //Get a worker object.
        StringBuilder myStringBuilder = new StringBuilder();

        //Open tags and write the top portion.       
        myStringBuilder.Append("<body>");
        myStringBuilder.Append("<table id='customers' width='100%' border='1px' cellpadding='5' cellspacing='0' ");
        myStringBuilder.Append("style='border: solid 1px #CCCCCC; font-size: x-small;'>");
        myStringBuilder.Append("<caption style='background-color: Orange;color: black;padding-top:5px; " +
            " padding-bottom:4px;font-weight: bold;'>" + strTitle + "</caption>");
        //Add the headings row.
        myStringBuilder.Append("<tr align='left' valign='top'>");
        foreach (DataColumn myColumn in sentDataTable.Columns)
        {
            myStringBuilder.Append("<th style='font-size:1.1em;text-align:left;padding-top:5px;padding-bottom:4px;background-color:#99CC66;color:#ffffff;'>");
            myStringBuilder.Append("<b>" + myColumn.ColumnName + "</b>");
            myStringBuilder.Append("</th>");
        }

        myStringBuilder.Append("</tr>");

        //Add the data rows.
        int intaltRow = 0;
        foreach (DataRow myRow in sentDataTable.Rows)
        {
            string strCss = "";
            string strCss1 = "";
            if (myRow[0].ToString().Contains("*") == true)
            {
                strCss = " style='font-weight: bold;color:#000000;background-color:#CCCC66;'";
                strCss1 = " style='font-weight: bold;color:#000000;background-color:#A7C942;'";
            }
            else
            {
                strCss = " style='color:#000000;background-color:#EAF2D3;'";
            }
            if (intaltRow == 0)
            {
                intaltRow = 1;
                myStringBuilder.Append("<tr  " + strCss + " align='left' valign='top'>");

            }
            else
            {
                intaltRow = 0;
                myStringBuilder.Append("<tr  " + strCss1 + " align='left' valign='top'>");
            }
            foreach (DataColumn myColumn in sentDataTable.Columns)
            {
                myStringBuilder.Append("<td align='left' valign='top'>");
                myStringBuilder.Append(myRow[myColumn.ColumnName].ToString());
                myStringBuilder.Append("</td>");
            }

            myStringBuilder.Append("</tr>");
        }

        //Close tags.
        myStringBuilder.Append("<BR>");
        myStringBuilder.Append("<BR>");

        myStringBuilder.Append("</table>");
        myStringBuilder.Append("</body>");


        return myStringBuilder.ToString();
    }
    public double ConvertToNumber_Double(Object obj)
    {
        double RetVal = 0;
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                RetVal = double.Parse(obj.ToString());
        }
        catch { }
        return RetVal;
    }
    public Decimal ConvertToNumber_Dec(Object obj)
    {
        Decimal RetVal = 0;
        try
        {
            if (obj != null && obj.ToString().Trim() != "")
                RetVal = Decimal.Parse(obj.ToString());
        }
        catch { }
        return RetVal;
    }

    public String ConvertToString(Object obj)
    {
        String RetVal = "";
        try
        {
            if (String.IsNullOrEmpty(obj.ToString()) == true)
                RetVal = "";
            else
                RetVal = obj.ToString();
        }
        catch { RetVal = ""; }
        return RetVal;
    }

    public String NewCode(String TblNm, String FldNm, String FilterCond)
    {

        //***********************************************************//
        //* TblNm        - Table Name                               *//
        //* FldNm        - Field Name                               *//
        //* FilterCond   - Filter Condition                         *//
        //***********************************************************//
        String RetVal = "1";
        String strSql = "";
        DataTable dtData = new DataTable();
        FilterCond = FilterCond.Trim();
        if (FilterCond != "") strSql = " where " + FilterCond;
        if (mGlobal.DBType == "A")
            strSql = "select max(val(" + FldNm + ")) from " + TblNm + strSql;
        if (mGlobal.DBType == "S")
            strSql = "select MAX(CASE WHEN  ISNUMERIC(" + FldNm + ")=1 THEN  (" + FldNm + ") ELSE 0 END) from " + TblNm + strSql;
        else
            strSql = "select max(val(" + FldNm + ")) from " + TblNm + strSql;
        dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
        if (dtData.Rows.Count > 0)
        {
            if (dtData.Rows[0][0].ToString().Trim() != "")
                RetVal = (Int32.Parse(dtData.Rows[0][0].ToString().Trim()) + 1).ToString();
        }
        return RetVal;
    }
    public DateTime FormatDate(String vDate, String vFormat)
    {
        DateTime dt = DateTime.Now;

        try { dt = DateTime.Parse(vDate); }
        catch { }
        try { return DateTime.Parse(dt.ToString(vFormat)); }
        catch { return DateTime.Now; }
    }
    public String FormatDateS(String vDate, String vFormat)
    {
        DateTime dt = DateTime.Now;

        try { dt = DateTime.Parse(vDate); }
        catch { }

        string myDate = dt.ToString(vFormat);
        return myDate;
    }
    public bool isDate(DateTime dt)
    {
        /******** Checking is a valid date or not **********/
        bool boolRet = false;
        int intDay = 0, intMonth = 0, intYear = 0;
        try
        {
            intDay = dt.Day;
            intMonth = dt.Month;
            intYear = dt.Year;
            if (((intDay > 0) && (intDay < 32)) && ((intMonth > 0) && (intMonth < 32)) && (intYear.ToString().Length == 4))
                boolRet = true;
        }
        catch (Exception ex) { }
        return boolRet;
    }
    public bool CreateDir(String vDirPath)
    {
        try
        {
            Directory.CreateDirectory(vDirPath);
            return true;
        }
        catch { return false; }
    }
    public String FormatDBDate(String vDate)
    {
        DateTime dt = DateTime.Now;
        try { dt = DateTime.Parse(vDate); }
        catch { }
        String myDate = dt.ToShortDateString();

        if (mGlobal.DBType == "A")
            myDate = "cdate('" + dt.ToString("dd/MM/yyyy") + "')";
        else if (mGlobal.DBType == "M")
            myDate = "'" + dt.ToString("yyyy/MM/dd") + "'";
        else
            myDate = "'" + dt.ToString("MM/dd/yyyy") + "'";
        return myDate;
    }
    public String FormatDBServDate(String vDate)
    {
        DateTime dt = DateTime.Now;
        try { dt = DateTime.Parse(vDate); }
        catch { }
        String myDate = dt.ToShortDateString();

        if (mGlobal.ServerDBType == "A")
            myDate = "cdate('" + dt.ToString("dd/MM/yyyy") + "')";
        else if (mGlobal.ServerDBType == "M")
            myDate = "'" + dt.ToString("yyyy/MM/dd") + "'";
        else
            myDate = "'" + dt.ToString("MM/dd/yyyy") + "'";
        return myDate;
    }

    public String FormatDBServDateTime(string   vDate)
    {
        DateTime dt = DateTime.Now;
        try { dt = DateTime.Parse(vDate.ToString()); }
        catch { }
        String myDate = dt.ToShortDateString();

        if (mGlobal.ServerDBType == "A")
            myDate = "cdate('" + dt.ToString("dd/MM/yyyy") + "')";
        else if (mGlobal.ServerDBType == "M")
            myDate = "'" + dt.ToString("yyyy/MM/dd") + "'";
        else
            myDate = "'" + dt.ToString("MM/dd/yyyy") + "'";
        myDate = FormatDBServDateWithTime(vDate);
        return myDate;
    }

    


    public String FormatDBServDate(String vDate, bool WithoutQuats)
    {
        if (WithoutQuats)
            return FormatDBServDate(vDate).Replace("'", "");
        else
            return FormatDBServDate(vDate);
    }

    // General Settings Access Methods Starts.......
    public void LoadGenSettDataAll()
    {
        mdtGenSett = mGlobal.LocalDBCon.ExecuteQuery("select * from gensett");
    }
    public Object GetGenSettData(String _FldName)
    {
        return mdtGenSett.Rows[0][_FldName];
    }
    public void ReleaseGenSettDataAll()
    {
        mdtGenSett.Dispose();
        mdtGenSett = null;
    }
    public Object GetGenSettFieldData(String _FldName)
    {
        DataTable dtGenSett = mGlobal.LocalDBCon.ExecuteQuery("select " + _FldName + " from gensett");
        return dtGenSett.Rows[0][_FldName];
    }

    public Object GetSingleFieldData(String _FldName, String _TblName, String _FliterCondition)
    {
        DataTable dtGenSett = mGlobal.LocalDBCon.ExecuteQuery("select " + _FldName + " from " + _TblName + " " + _FliterCondition);
        return dtGenSett.Rows[0][_FldName];
    }


    public Object GetPermanentCodeFldData(String strMod)
    {
        DataTable dtGenSett = mGlobal.LocalDBCon.ExecuteQuery("select p_code from Permanentcode where p_mod='" + strMod.Trim() + "'");
        return dtGenSett.Rows[0]["p_code"];
    }


    public void SetGenSettFieldData(String _FldName, String _FldValue)//For string value type
    {
        mGlobal.LocalDBCon.ExecuteNonQuery("update gensett set " + _FldName + "='" + _FldValue + "'");
    }

    public void SetGenSettFieldData(String _FldName, String _FldValue, bool boolNonStringValue)//For string value type
    {
        mGlobal.LocalDBCon.ExecuteNonQuery("update gensett set " + _FldName + "=" + _FldValue + "");
    }

    public void LoadGenSettings()
    {
        this.LoadGenSettDataAll();

        mGlobal.RunningDateMode = this.GetGenSettData("gs_rundtmode").ToString().ToUpper();
        mGlobal.CredentialsCheckOnEachCall = this.GetGenSettData("gs_credoneachcall").ToString().ToUpper() == "Y" ? true : false;

        try { mGlobal.StartDate = ((DateTime)this.GetGenSettData("gs_startdt")); }
        catch { }
        mGlobal.RefreshCredentials(true);

        // Firm Details
        DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select * from firm");
        if (dtData.Rows.Count > 0)
        {
            mGlobal.FirmName = dtData.Rows[0]["f_name"].ToString();
            mGlobal.FirmAddress = dtData.Rows[0]["f_add1"].ToString() + ", " + dtData.Rows[0]["f_add2"].ToString() + ", " + dtData.Rows[0]["f_place"].ToString();
        }

        // OP Details
        mGlobal.RegValidDays = int.Parse(this.GetGenSettData("gs_regvaliddays").ToString());
        mGlobal.RegFees = decimal.Parse(this.GetGenSettData("gs_regfee").ToString());
        this.ReleaseGenSettDataAll();
    }
    private void Update_PharmacySettings()
    {

    }

    // General Settings Access Methods Ends.......

    public String getReportColor(String strColorDesc)
    {
        if (mdtColor == null)
            mdtColor = mGlobal.LocalDBCon.ExecuteQuery("select * from Colortable order by c_slno");
        DataRow[] dr = mdtColor.Select("[c_mod]='" + strColorDesc.Trim() + "'");
        return dr[0]["c_color"].ToString();
    }

    public void AutoComplete(string strFldNm, string strTableNm, ref TextBox txtBoxId)
    {
        DataTable dtData = null;
        dtData = mGlobal.LocalDBCon.ExecuteQuery("SELECT max(" + strFldNm + ") AS  fld   FROM  " +
            " " + strTableNm + "  GROUP BY  " + strFldNm + " order by " + strFldNm);
        AutoCompleteStringCollection acData = new AutoCompleteStringCollection();
        foreach (DataRow dr in dtData.Rows)
        {
            acData.Add(dr["fld"].ToString());
        }
        txtBoxId.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtBoxId.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtBoxId.AutoCompleteCustomSource = acData;
        dtData.Dispose();
    }
    public void AutoComplete(string strFldNm, string strTableNm,ref TextBox txtBoxId,string str, AutoCompleteMode acMode, bool boolANs)
    {
        DataTable dtData = null;
        dtData = mGlobal.LocalDBCon.ExecuteQuery("SELECT max(" + strFldNm + ") AS  fld   FROM  " +
            " " + strTableNm + "  GROUP BY  " + strFldNm + " order by " + strFldNm);
        AutoCompleteStringCollection acData = new AutoCompleteStringCollection();
        foreach (DataRow dr in dtData.Rows)
        {
            acData.Add(dr["fld"].ToString());
        }
        txtBoxId.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtBoxId.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtBoxId.AutoCompleteCustomSource = acData;
        dtData.Dispose();
    }
    public void AutoCompleet(string strFldNm, string strTableNm, TextBox txtBoxId,string strCondition)
    {
        DataTable dtData = null;
        dtData = mGlobal.LocalDBCon.ExecuteQuery("SELECT max(" + strFldNm + ") AS  fld   FROM  " +
            " " + strTableNm + " " +  strCondition+ " GROUP BY  " + strFldNm + " order by " + strFldNm);
        AutoCompleteStringCollection acData = new AutoCompleteStringCollection();
        foreach (DataRow dr in dtData.Rows)
        {
            acData.Add(dr["fld"].ToString());
        }
        txtBoxId.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtBoxId.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtBoxId.AutoCompleteCustomSource = acData;
        dtData.Dispose();
    }
    public void AutoCompleet(string strFldNm, string strTableNm, TextBox txtBoxId)
    {
        DataTable dtData = null;
        dtData = mGlobal.LocalDBCon.ExecuteQuery("SELECT max(" + strFldNm + ") AS  fld   FROM  " +
            " " + strTableNm + "  GROUP BY  " + strFldNm + " order by " + strFldNm);
        AutoCompleteStringCollection acData = new AutoCompleteStringCollection();
        foreach (DataRow dr in dtData.Rows)
        {
            acData.Add(dr["fld"].ToString());
        }
        txtBoxId.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtBoxId.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtBoxId.AutoCompleteCustomSource = acData;
        dtData.Dispose();
    }
    public void AutoCompleet(string strFldNm, string strTableNm, TextBox txtBoxId,AutoCompleteMode acMode,bool boolANs)
    {
        DataTable dtData = null;
        dtData = mGlobal.LocalDBCon.ExecuteQuery("SELECT max(" + strFldNm + ") AS  fld   FROM  " +
            " " + strTableNm + "  GROUP BY  " + strFldNm + " order by " + strFldNm);
        AutoCompleteStringCollection acData = new AutoCompleteStringCollection();
        foreach (DataRow dr in dtData.Rows)
        {
            acData.Add(dr["fld"].ToString());
        }
        txtBoxId.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtBoxId.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtBoxId.AutoCompleteCustomSource = acData;
        dtData.Dispose();
    }

    public DataTable ConvertRowCollToDataTable(DataRow[] _drcData, DataTable _dtSrc)
    {
        DataTable dtData = _dtSrc.Clone();
        dtData.Rows.Clear();
        //dtData.Clear();
        try
        {
            if (_drcData.Length > 0)
            {
                //for (int intRow = 0; intRow < _drcData.Length; intRow++)
                foreach (DataRow dr in _drcData)
                {
                    dtData.ImportRow(dr);
                }
            }
        }
        catch (Exception ex) { }
        return dtData;
    }

    public DataTable Filter_Datatable(DataTable _dtSrc, List<String> _FieldColl, String _Filter, String _Sort)
    {
        int intCnt = 0;
        int intCnt2 = 0;
        DataTable dtData = new DataTable();
        try
        {
            for (intCnt = 0; intCnt < _dtSrc.Columns.Count; intCnt++)
            {
                foreach (String strCol in _FieldColl)
                {
                    if (_dtSrc.Columns[intCnt].ColumnName.ToLower() == strCol.ToLower())
                    {
                        dtData.Columns.Add(strCol, _dtSrc.Columns[intCnt].DataType);
                        //_FieldColl.Remove(strCol);
                    }
                }
            }
            //_FieldColl.c
            DataRow[] _drcData = _dtSrc.Select(_Filter, _Sort);
            if (_drcData.Length > 0)
            {
                //for (int intRow = 0; intRow < _drcData.Length; intRow++)
                foreach (DataRow dr in _drcData)
                {
                    dtData.ImportRow(dr);
                }
            }
        }
        catch (Exception ex) { }
        return dtData;
    }

    public void ChangeDatatable_DateColumnsToString(ref DataTable _dtData)
    {
        String strColNm = "";
        for (int intCol = 0; intCol < _dtData.Columns.Count; intCol++)
        {
            DataColumn dc = _dtData.Columns[intCol];
            if (dc.DataType == typeof(System.DateTime))
            {
                strColNm = dc.ColumnName;
                _dtData.Columns.Remove(dc);
                _dtData.Columns.Add(strColNm, typeof(System.String));
            }
        }
    }
    public String FormatDBServDateWithTime(String vDate, string strAMorPM)
    {
        string vTimeString = "";
        if (strAMorPM.ToUpper() == "AM")
            vTimeString = "12:00:00 Am";
        else
            vTimeString = "23:59:59 Pm";
        string strRtn = "";
        if (mGlobal.ServerDBType == "A")
            strRtn = "cdate('" + vDate.Substring(0, 2) + "/" + vDate.Substring(3, 2) +
                "/" + vDate.Substring(6, 4) + "')";
        else if (mGlobal.ServerDBType == "M")
            strRtn = "'" + vDate.Substring(6, 4) + "/" + vDate.Substring(3, 2) + "/" + vDate.Substring(0, 2) + "'";
        else
            strRtn = "'" + vDate.Substring(3, 2) + "/" + vDate.Substring(0, 2) + "/" + vDate.Substring(6, 4) + " " + vTimeString + "'";
        return strRtn;
    }

    public String FormatDBServDateWithTime(String vDate)
    {
        string strRtn = "'" + vDate.Substring(3, 2) + "/" + vDate.Substring(0, 2) + "/" +
            vDate.Substring(6, 4) + " " +
            vDate.Substring(11, vDate.Length - 11) + "'";
        return strRtn;
    }

    // ***************Change local Date Format ***************
    public void ChangeDateFormatTo_DD_MM_YYYY()
    {
        try
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            key.SetValue("sShortDate", "dd/MM/yyyy", Microsoft.Win32.RegistryValueKind.Unknown);
            //key.GetValue("sShortDate").ToString();
        }
        catch { }
    }
    // *******************************************************

    public String DataRowToText(DataRow dr, bool TruncateColumnPrefix)
    {
        string strRet = "";
        try
        {
            for (int intCols = 0; intCols < dr.ItemArray.Length; intCols++)
            {
                strRet += dr[intCols].ToString() + char.ConvertFromUtf32(250);
            }
        }
        catch (Exception ex) { }
        return strRet;
    }
    #region Export to Excel
    /// <summary>
    /// Exports a passed datagridview to an Excel worksheet.
    /// If captions is true, grid headers will appear in row 1.
    /// Data will start in row 2.
    /// </summary>
    /// <param name="datagridview"></param>
    /// <param name="captions"></param>
    public void Export2Excel(ref DataGridView datagridview, bool captions)
    {
        object objApp_Late;
        object objBook_Late;
        object objBooks_Late;
        object objSheets_Late;
        object objSheet_Late;
        object objRange_Late;
        object[] Parameters;
        string[] headers = new string[datagridview.ColumnCount];
        string[] columns = new string[datagridview.ColumnCount];

        int i = 0;
        int c = 0;
        for (c = 0; c < datagridview.ColumnCount; c++)
        {
            headers[c] = datagridview.Rows[0].Cells[c].OwningColumn.Name.ToString();
            i = c + 65;
            columns[c] = Convert.ToString((char)i);
        }

        try
        {
            // Get the class type and instantiate Excel.
            Type objClassType;
            objClassType = Type.GetTypeFromProgID("Excel.Application");
            objApp_Late = Activator.CreateInstance(objClassType);
            //Get the workbooks collection.
            objBooks_Late = objApp_Late.GetType().InvokeMember("Workbooks",
            BindingFlags.GetProperty, null, objApp_Late, null);
            //Add a new workbook.
            objBook_Late = objBooks_Late.GetType().InvokeMember("Add",
            BindingFlags.InvokeMethod, null, objBooks_Late, null);
            //Get the worksheets collection.
            objSheets_Late = objBook_Late.GetType().InvokeMember("Worksheets",
            BindingFlags.GetProperty, null, objBook_Late, null);
            //Get the first worksheet.
            Parameters = new Object[1];
            Parameters[0] = 1;
            objSheet_Late = objSheets_Late.GetType().InvokeMember("Item",
            BindingFlags.GetProperty, null, objSheets_Late, Parameters);

            if (captions)
            {
                // Create the headers in the first row of the sheet
                for (c = 0; c < datagridview.ColumnCount; c++)
                {
                    //Get a range object that contains cell.
                    Parameters = new Object[2];
                    Parameters[0] = columns[c] + "1";
                    Parameters[1] = Missing.Value;
                    objRange_Late = objSheet_Late.GetType().InvokeMember("Range",
                    BindingFlags.GetProperty, null, objSheet_Late, Parameters);
                    //Write Headers in cell.
                    Parameters = new Object[1];
                    Parameters[0] = headers[c];
                    objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty,
                    null, objRange_Late, Parameters);
                }
            }

            // Now add the data from the grid to the sheet starting in row 2
            for (i = 0; i < datagridview.RowCount; i++)
            {
                for (c = 0; c < datagridview.ColumnCount; c++)
                {
                    //Get a range object that contains cell.
                    Parameters = new Object[2];
                    Parameters[0] = columns[c] + Convert.ToString(i + 2);
                    Parameters[1] = Missing.Value;
                    objRange_Late = objSheet_Late.GetType().InvokeMember("Range",
                    BindingFlags.GetProperty, null, objSheet_Late, Parameters);
                    //Write Headers in cell.
                    Parameters = new Object[1];
                    Parameters[0] = datagridview.Rows[i].Cells[headers[c]].Value.ToString();
                    Parameters[0] = datagridview.Rows[i].Cells[headers[c]].FormattedValue.ToString();//Ats
                    objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty,
                    null, objRange_Late, Parameters);
                }
            }

            //Return control of Excel to the user.
            Parameters = new Object[1];
            Parameters[0] = true;
            objApp_Late.GetType().InvokeMember("Visible", BindingFlags.SetProperty,
            null, objApp_Late, Parameters);
            objApp_Late.GetType().InvokeMember("UserControl", BindingFlags.SetProperty,
            null, objApp_Late, Parameters);
        }
        catch (Exception theException)
        {
            String errorMessage;
            errorMessage = "Error: ";
            errorMessage = String.Concat(errorMessage, theException.Message);
            errorMessage = String.Concat(errorMessage, " Line: ");
            errorMessage = String.Concat(errorMessage, theException.Source);

            MessageBox.Show(errorMessage, "Error");
        }
    }
    #endregion
    public void ClearAndInitObjects(Control.ControlCollection _Obj)
    {
        try
        {
            foreach (Control ctlObj in _Obj)// Searhing all controls 
            {
                try
                {
                    if ((ctlObj is Panel) || (ctlObj is GroupBox))
                    {
                        ClearAndInitObjects(ctlObj.Controls);
                    }
                    else if (ctlObj is TextBox)//Clearing the text of each textbox
                        ctlObj.Text = "";
                    else if (ctlObj is ComboBox)//Setting index to zero of each combo
                        ((ComboBox)ctlObj).SelectedIndex = 0;
                    else if (ctlObj is UserControls.SimpleComboSearch)//Setting index to zero of each SimpleComboSearch
                        ((UserControls.SimpleComboSearch)ctlObj).Visible = true;
                    else if (ctlObj is DateTimePicker)//Setting current date for each DateTimePicker
                        ((DateTimePicker)ctlObj).Value = DateTime.Now.Date;
                }
                catch { }
            }
        }
        catch { }
    }
    public void ComboSetText(ref ComboBox _cbo, String _Text)
    {
        try { _cbo.SelectedIndex = 0; }
        catch { }
        try { _cbo.Text = _Text; }
        catch { }
    }
    public void DatePickerSetDate(ref DateTimePicker _dtp, Object _ObjValue)
    {
        try
        {
            _dtp.Value = DateTime.Now;
            _dtp.Value = Convert.ToDateTime(_ObjValue.ToString());
        }
        catch { }
        try
        {
            if (_ObjValue == null || _ObjValue == DBNull.Value)
                _dtp.Checked = false;
            else
                _dtp.Checked = true;
        }
        catch { }
    }



    public void CheckBoxListSelect(string _valueToSelect, string _textToSelect, ref System.Windows.Forms.CheckedListBox _chklst)
    {
        for (int intCnt = 0; intCnt < _chklst.Items.Count; intCnt++)
        {
            DataRowView drvRow = (DataRowView)_chklst.Items[intCnt];
            if (_valueToSelect == ConvertToString(drvRow[_chklst.ValueMember]))
            {
                _chklst.SetItemChecked(intCnt, true);
            }
        }
    }

    public void CheckBoxListClearSelect(ref System.Windows.Forms.CheckedListBox _chklst)
    {
        for (int intCnt = 0; intCnt < _chklst.Items.Count; intCnt++)
        {
            _chklst.SetItemChecked(intCnt, false);
        }
    }

    internal DateTime FormatDBDate(string p, string p_2)
    {
        throw new Exception("The method or operation is not implemented.");
    }
}