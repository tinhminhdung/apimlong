using Entity;
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

public class ThanhToanDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static bool SaveBankAccount(obj_ThanhVienNganHang obj, string IDThanhVien)
    {
        try
        {
            using (var con = new SqlConnection(strConn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("S_SaveBankAccount_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IDThanhVien", IDThanhVien));
                cmd.Parameters.Add(new SqlParameter("@HoVaTen", obj.HoVaTen));
                cmd.Parameters.Add(new SqlParameter("@SoTaiKhoan", obj.SoTaiKhoan));
                cmd.Parameters.Add(new SqlParameter("@TenNganHang", obj.TenNganHang));
                cmd.Parameters.Add(new SqlParameter("@BankNameShort", obj.BankNameShort));
                cmd.Parameters.Add(new SqlParameter("@ChiNhanh", obj.ChiNhanh));

                var reader = cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_SaveBankAccount_Insert");
            return false;
        }
    }

    #region GET BY ID
    public static List<obj_ThanhVienNganHang> GETBYID(string Id)
    {
        SqlConnection conn = Database.Connection();
        SqlCommand comm = new SqlCommand("select * from ThanhVienNganHang where IDThanhVien=" + Id + " order by id asc", conn);
        comm.CommandType = CommandType.Text;
        try
        {
            return Database.Bind_List_Reader<obj_ThanhVienNganHang>(comm);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion

    #region ThanhVienNganHang_ADMIN_Detail
    public static List<obj_ThanhVienNganHang> ThanhVienNganHang_ADMIN_Detail()
    {
        List<obj_ThanhVienNganHang> it_r = new List<obj_ThanhVienNganHang>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from menu where capp='NH' order by Orders asc", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    obj_ThanhVienNganHang it = new obj_ThanhVienNganHang
                    {
                        HoVaTen = reader["Name"].ToString(),
                        SoTaiKhoan = reader["Titleseo"].ToString(),
                        TenNganHang = reader["Description"].ToString(),
                        BankNameShort = reader["Meta"].ToString(),
                        ChiNhanh = reader["Keyword"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_News_Detail");
                return it_r;
            }
        }
    }


    #endregion

    #region Insert_RutTien_NganHang
    public static int Insert_RutTien_NganHang(obj_RutTienNganHangAdd obj)
    {
        using (SqlCommand dbCmd = new SqlCommand("insert RutTien_NganHang values(@IDThanhVien,@TongTienThanhToan,@TenNganHang,@SoTaiKhoan,@HoVaTenTK,@NgayTao,@TrangThai)", Database.Connection()))
        {
            dbCmd.CommandType = CommandType.Text;
            dbCmd.Parameters.Add(new SqlParameter("@IDThanhVien", obj.IDThanhVien));
            dbCmd.Parameters.Add(new SqlParameter("@TongTienThanhToan", obj.TongTienThanhToan));
            dbCmd.Parameters.Add(new SqlParameter("@TenNganHang", obj.TenNganHang));
            dbCmd.Parameters.Add(new SqlParameter("@SoTaiKhoan", obj.SoTaiKhoan));
            dbCmd.Parameters.Add(new SqlParameter("@HoVaTenTK", obj.HoVaTenTK));
            dbCmd.Parameters.Add(new SqlParameter("@NgayTao", obj.NgayTao));
            dbCmd.Parameters.Add(new SqlParameter("@TrangThai", obj.TrangThai));
            dbCmd.ExecuteNonQuery();
        }
        System.Web.HttpContext.Current.Cache.Remove("RutTien_NganHang");
        using (SqlCommand dbCmd = new SqlCommand("select isnull(max(ID),0) as maxid from RutTien_NganHang", Database.Connection()))
            return Convert.ToInt32(Database.GetData(dbCmd).Rows[0]["maxid"]);
    }
    #endregion

    #region RutTien_GetDetail
    public static List<obj_RutTienNganHangView> RutTien_GetDetail(string id)
    {
        List<obj_RutTienNganHangView> it_r = new List<obj_RutTienNganHangView>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from RutTien_NganHang where id=" + id + "", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    obj_RutTienNganHangView it = new obj_RutTienNganHangView
                    {
                        ID = Convert.ToInt32(reader["ID"].ToString()),
                        TongTienThanhToan = reader["TongTienThanhToan"].ToString(),
                        TenNganHang = reader["TenNganHang"].ToString(),
                        SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                        HoVaTenTK = reader["HoVaTenTK"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_News_Detail");
                return it_r;
            }
        }
    }

    #endregion

    #region Insert_QRCode
    public static int Insert_QRCode(AddThanhToanQRCode obj)
    {
        using (SqlCommand dbCmd = new SqlCommand("insert ThanhToanQRCode values(@IDNguoiThanhToan,@IDNguoiNhan,@SoTienThanhToan,@NoiDung,@UserDisplayName,@NgayTao)", Database.Connection()))
        {
            dbCmd.CommandType = CommandType.Text;
            dbCmd.Parameters.Add(new SqlParameter("@IDNguoiThanhToan", obj.IDNguoiThanhToan));
            dbCmd.Parameters.Add(new SqlParameter("@IDNguoiNhan", obj.IDNguoiNhan));
            dbCmd.Parameters.Add(new SqlParameter("@SoTienThanhToan", obj.SoTienThanhToan));
            dbCmd.Parameters.Add(new SqlParameter("@NoiDung", obj.NoiDung));
            dbCmd.Parameters.Add(new SqlParameter("@UserDisplayName", obj.UserDisplayName));
            dbCmd.Parameters.Add(new SqlParameter("@NgayTao", obj.NgayTao));
            dbCmd.ExecuteNonQuery();
        }
        System.Web.HttpContext.Current.Cache.Remove("ThanhToanQRCode");
        using (SqlCommand dbCmd = new SqlCommand("select isnull(max(ID),0) as maxid from ThanhToanQRCode", Database.Connection()))
            return Convert.ToInt32(Database.GetData(dbCmd).Rows[0]["maxid"]);
    }
    #endregion
}
