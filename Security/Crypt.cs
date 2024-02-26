using System;
using System.Data;
using System.Configuration;
using System.Collections;

/// <summary>
/// It is for encription and decription
/// </summary>
public class Crypt
{
	public Crypt()
	{
	}
    public String EncryptData(String vData)
    {
        String strRet="";
        String strTmp1 = "";
        char c;
        char[] ca;
        int intAsc;
        for (int intCnt = 0; intCnt < vData.Length; intCnt++)
        {
            ca = vData.Substring(intCnt,1).ToCharArray();
            c = ca[0];
            intAsc = ((int)c)+12;
            strTmp1 = Convert.ToChar(intAsc - 10).ToString() + Convert.ToChar(intAsc - 5).ToString() + Convert.ToChar(intAsc).ToString() + Convert.ToChar(intAsc - 30).ToString() + Convert.ToChar(intAsc - 7).ToString();           
            strRet = strRet + strTmp1;
        }
        return strRet;
    }
    //public String DecryptData(String vData)
    //{
    //    int intCnt;
    //    int intHCode;
    //    String strTmp1 = "";
    //    String strRet = "";
    //    for (intCnt = 0; intCnt < vData.Length; intCnt = intCnt+10)
    //    {
    //        strTmp1 = vData.Substring(intCnt, 10).ToString();
    //        intHCode = int.Parse(strTmp1) - 12;
    //        strRet = strRet + (intHCode.ToString() );
    //    }
    //    return strRet;
    //}
}
