using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;
using WebAPI.Models;

public class ThanhVienDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static bool Save_ThanhVien(Member obj)
    {
        try
        {
            using (var con = new SqlConnection(strConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("S_Member_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDThanhVien", obj.IDThanhVien));
                cmd.Parameters.Add(new SqlParameter("@PasWord", obj.PasWord));
                cmd.Parameters.Add(new SqlParameter("@HoVaTen", obj.HoVaTen));
                cmd.Parameters.Add(new SqlParameter("@GioiTinh", obj.GioiTinh));
                //cmd.Parameters.Add(new SqlParameter("@NgaySinh", obj.NgaySinh));
                cmd.Parameters.Add(new SqlParameter("@DiaChi", obj.DiaChi));
                cmd.Parameters.Add(new SqlParameter("@DienThoai", obj.DienThoai));
                cmd.Parameters.Add(new SqlParameter("@Email", obj.Email));
                cmd.Parameters.Add(new SqlParameter("@AnhDaiDien", obj.AnhDaiDien));
               // cmd.Parameters.Add(new SqlParameter("@NgayTao", obj.NgayTao));
                cmd.Parameters.Add(new SqlParameter("@Key", obj.Key));
                cmd.Parameters.Add(new SqlParameter("@TrangThai", obj.TrangThai));
                cmd.Parameters.Add(new SqlParameter("@Lang", obj.Lang));
                cmd.Parameters.Add(new SqlParameter("@GioiThieu", obj.GioiThieu));
                 cmd.Parameters.Add(new SqlParameter("@MTree", obj.MTree));
                cmd.Parameters.Add(new SqlParameter("@TongTien", obj.TongTien));

                var reader = cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_Member_Insert");
            return false;
        }
    }

}
