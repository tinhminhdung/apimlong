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

public class ThongBaoDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    
    public static List<Obj_GetCountWaiting> GetCountWaiting( string IDThanhVien)
    {
        List<Obj_GetCountWaiting> it_r = new List<Obj_GetCountWaiting>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_ThongBao_GetCountWaiting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("IDThanhVien", IDThanhVien));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Obj_GetCountWaiting it = new Obj_GetCountWaiting
                    {
                        Counts = Convert.ToInt32(reader["Counts"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_ThongBao_GetCountWaiting");
                return it_r;
            }
        }
    }
    public static List<DSThongBao> list_All_Active(string SkipCount, string MaxResultCount, string IDThanhVien)
    {
        List<DSThongBao> it_r = new List<DSThongBao>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_ThongBao_list_AllActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("SkipCount", SkipCount));
                cmd.Parameters.Add(new SqlParameter("MaxResultCount", MaxResultCount));
                cmd.Parameters.Add(new SqlParameter("IDThanhVien", IDThanhVien));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSThongBao it = new DSThongBao
                    {
                        id = Convert.ToInt32(reader["ThongBaoThanhVien"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        DaDoc = Convert.ToInt32(reader["Status"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_ThongBao_list_AllActive");
                return it_r;
            }
        }
    }
    public static List<DSThongBao> list_All_Active_Unread(string SkipCount, string MaxResultCount, string IDThanhVien, string Status)
    {
        List<DSThongBao> it_r = new List<DSThongBao>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_ThongBao_list_All_Active_Unread", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("SkipCount", SkipCount));
                cmd.Parameters.Add(new SqlParameter("MaxResultCount", MaxResultCount));
                cmd.Parameters.Add(new SqlParameter("IDThanhVien", IDThanhVien));
                cmd.Parameters.Add(new SqlParameter("Status", Status));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSThongBao it = new DSThongBao
                    {
                        id = Convert.ToInt32(reader["ThongBaoThanhVien"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        DaDoc = Convert.ToInt32(reader["Status"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_ThongBao_list_AllActive");
                return it_r;
            }
        }
    }


    
    public static List<DSThongBao> list_Lastest_Active(string Size, string IDThanhVien)
    {
        List<DSThongBao> it_r = new List<DSThongBao>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_ThongBao_listLastestActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Size", Size));
                cmd.Parameters.Add(new SqlParameter("IDThanhVien", IDThanhVien));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSThongBao it = new DSThongBao
                    {
                        id = Convert.ToInt32(reader["ThongBaoThanhVien"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        DaDoc = Convert.ToInt32(reader["Status"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_ThongBao_listLastestActive");
                return it_r;
            }
        }
    }

    public static List<TB_Detail> New_Detail(string id,string IDThanhVien)
    {
        List<TB_Detail> it_r = new List<TB_Detail>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_ThongBao_Detail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("IDThanhVien", IDThanhVien));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TB_Detail it = new TB_Detail
                    {
                        id = Convert.ToInt32(reader["ThongBaoThanhVien"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        NoiDung = reader["NoiDung"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_ThongBao_Detail");
                return it_r;
            }
        }
    }

    public static List<DSThongBao> list_Related(string Id, string Size,string IDThanhVien)
    {
        List<DSThongBao> it_r = new List<DSThongBao>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_ThongBao_list_Related1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Id", Id));
                cmd.Parameters.Add(new SqlParameter("Size", Size));
                cmd.Parameters.Add(new SqlParameter("IDThanhVien", IDThanhVien));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSThongBao it = new DSThongBao
                    {
                        id = Convert.ToInt32(reader["ThongBaoThanhVien"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        DaDoc = Convert.ToInt32(reader["Status"].ToString()),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_ThongBao_list_Related1");
                return it_r;
            }
        }
    }


   /// Thông báo cho từng sự kiện
    public static List<CHThongBao> GETBYID(string Id)
    {
        SqlConnection conn = Database.Connection();
        SqlCommand comm = new SqlCommand("S_CHThongBao_GetById", conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add(new SqlParameter("@ID", Id));
        try
        {
            return Database.Bind_List_Reader<CHThongBao>(comm);
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
}
