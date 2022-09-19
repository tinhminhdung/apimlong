using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC;
using WebAPI.Models;

public class Commond
{
    public static string Fomart_Price(string money)
    {
        if (money.Length > 0)
        {
            double value = Convert.ToDouble(money.ToString());
            string str = value.ToString("#,##,##");
            return str.Replace(",", ",");
        }
        else
        {
            return "0";
        }
    }
    public static bool Password(string pass)
    {
        //return System.Text.RegularExpressions.Regex.IsMatch(pass, @"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{8,}$");
        //return System.Text.RegularExpressions.Regex.IsMatch(pass, @"^(?=(.*[a-z]){1,})(?=(.*[0-9]){1,}).{8,}$");
        return System.Text.RegularExpressions.Regex.IsMatch(pass, @"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){0,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){0,}).{8,}$");
    }
    public static void CongTien(string IDThanhVien, string SoTiens)
    {
        #region Cộng tiền
        List<WebAPI.Models.Member> item = SMember.Name_Text("select * from Members  where ID=" + IDThanhVien + " ");
        if (item.Count > 0)
        {
            double SoTien = Convert.ToDouble(SoTiens);
            #region Cộng Ví Chính
            double TongTien = Convert.ToDouble(item[0].TongTien);

            double SumTongTien = 0;
            SumTongTien = ((TongTien) + (SoTien));

            SMember.Name_Text("update Members set TongTien=" + SumTongTien.ToString() + " where ID=" + IDThanhVien + "");

            #endregion
        }
        #endregion
    }
    public static string Setting(string giatri)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string item = "";
        Setting str = db.Settings.SingleOrDefault(p => p.Properties == giatri && p.Lang == "VIE");
        if (str != null)
        {
            item = str.Value;
        }
        return item.ToString();
    }
    public static string Show_Ten_ThanhVien(string id)
    {
        string str = "";
        List<WebAPI.Models.Member> dt = SMember.GETBYID(id);
        if (dt.Count >= 1)
        {
            str = dt[0].HoVaTen.ToString();
        }
        return str;
    }
}