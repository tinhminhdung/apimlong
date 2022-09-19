using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class BatDongSanDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static List<DSBatDongSan> list_All_Active(string SkipCount, string MaxResultCount)
    {
        List<DSBatDongSan> it_r = new List<DSBatDongSan>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_BatDongSan_list_AllActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("SkipCount", SkipCount));
                cmd.Parameters.Add(new SqlParameter("MaxResultCount", MaxResultCount));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSBatDongSan it = new DSBatDongSan
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        NamDuAn = reader["NamDuAn"].ToString(),
                        TienDuAn = reader["TienDuAn"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_BatDongSan_list_AllActive");
                return it_r;
            }
        }
    }

    public static List<DSBatDongSan> list_Lastest_Active(string Size)
    {
        List<DSBatDongSan> it_r = new List<DSBatDongSan>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_BatDongSan_listLastestActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Size", Size));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSBatDongSan it = new DSBatDongSan
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        NamDuAn = reader["NamDuAn"].ToString(),
                        TienDuAn = reader["TienDuAn"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_BatDongSan_listLastestActive");
                return it_r;
            }
        }
    }

    public static List<BDS_Detail> New_Detail(string id)
    {
        List<BDS_Detail> it_r = new List<BDS_Detail>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_BatDongSan_Detail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", id));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BDS_Detail it = new BDS_Detail
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        NoiDung = reader["NoiDung"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        NamDuAn = reader["NamDuAn"].ToString(),
                        TienDuAn = reader["TienDuAn"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_BatDongSan_Detail");
                return it_r;
            }
        }
    }

    public static List<DSBatDongSan> list_Related(string Id, string Size)
    {
        List<DSBatDongSan> it_r = new List<DSBatDongSan>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_BatDongSan_list_Related1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Id", Id));
                cmd.Parameters.Add(new SqlParameter("Size", Size));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSBatDongSan it = new DSBatDongSan
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        NamDuAn = reader["NamDuAn"].ToString(),
                        TienDuAn = reader["TienDuAn"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_BatDongSan_list_Related1");
                return it_r;
            }
        }
    }

}
