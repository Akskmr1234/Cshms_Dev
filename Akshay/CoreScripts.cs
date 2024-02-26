using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    class CoreScripts
    {
        CommFuncs mclsCFunc = new CommFuncs();
Global mGlobal = new Global();
        string SQL = "";
public DataTable getQueries(string Mode,string Type)
        {
            try
            {
                SQL = @" select * from core_scripts where mode='" + Mode + "' and type='" + Type + "' and active='Y' order by serial_numbers ASC";
                
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                return dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

    }
}
