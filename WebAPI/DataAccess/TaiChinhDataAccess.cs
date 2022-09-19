using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class TaiChinhDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static List<API_TaiChinh> ListPackagesActive()
    {
        List<API_TaiChinh> it_r = new List<API_TaiChinh>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Menu  where status=1  and capp='TC' order by Orders asc ", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    API_TaiChinh it = new API_TaiChinh
                    {
                        ID = Convert.ToInt32(reader["id"].ToString()),
                        TieuDe = reader["Name"].ToString(),
                        MoTa = reader["Description"].ToString(),
                        QuyenLoi = reader["Meta"].ToString(),
                        TenMucDauTu = reader["Titleseo"].ToString(),
                        ImageUrl = reader["Images"].ToString(),
                        NgayTao = DateTime.Parse(reader["Create_Date"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "ListPackagesActive");
                return it_r;
            }
        }
    }
    public static List<MucDauTuList> Get_MucDauTuList(string id)
    {
        List<MucDauTuList> it_r = new List<MucDauTuList>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select * from  [dbo].[TaiChinh] where icid=" + id + "   order by Create_Date asc ", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MucDauTuList it = new MucDauTuList
                    {
                        ID = Convert.ToInt32(reader["inid"].ToString()),
                        TieuDe = reader["Title"].ToString(),
                        SoTien = reader["Brief"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Get_MucDauTuList");
                return it_r;
            }
        }
    }
    public static List<KyHanList> Get_MucKyHan(string id)
    {
        List<KyHanList> it_r = new List<KyHanList>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select * from Menu  where status=1  and Parent_ID=" + id + " and capp='KH' order by Orders asc ", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    KyHanList it = new KyHanList
                    {
                        ID = Convert.ToInt32(reader["ID"].ToString()),
                        TyLe = reader["Name"].ToString(),
                        KyHan = reader["Noidung1"].ToString(),
                        TienLaiHangThang = reader["Noidung2"].ToString(),
                        PhanTramCTV = reader["Noidung3"].ToString(),
                        TongTienLai = reader["Noidung4"].ToString(),
                        LoaiKyHan = reader["Noidung5"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "Get_MucKyHan");
                return it_r;
            }
        }
    }

    public static List<Obj_combox> GetTienLaiThang(string id)
    {
        List<Obj_combox> it_r = new List<Obj_combox>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select * from Menu  where status=1  and ID=" + id + " and capp='KH' order by Orders asc ", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Obj_combox it = new Obj_combox
                    {
                        Code = reader["Noidung2"].ToString(),
                        Name = reader["Noidung2"].ToString(),

                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "GetTienLaiThang");
                return it_r;
            }
        }
    }

    public static List<Obj_combox> GetKyHan(string id)
    {
        List<Obj_combox> it_r = new List<Obj_combox>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Menu  where status=1  and ID=" + id + " and capp='KH' order by Orders asc ", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Obj_combox it = new Obj_combox
                    {
                        Code = reader["Noidung2"].ToString(),
                        Name = reader["Noidung1"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "GetTienLaiThang");
                return it_r;
            }
        }
    }
}
