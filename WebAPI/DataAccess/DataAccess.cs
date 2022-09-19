using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class DataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static List<TinTuc> list_All_Active(string SkipCount, string MaxResultCount)
    {
        List<TinTuc> it_r = new List<TinTuc>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_News_list_AllActive1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("SkipCount", SkipCount));
                cmd.Parameters.Add(new SqlParameter("MaxResultCount", MaxResultCount));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TinTuc it = new TinTuc
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),

                        
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_News_list_AllActive");
                return it_r;
            }
        }
    }


    public static List<TinTuc> List_NewsBy_Category(int categoryId, string SkipCount, string MaxResultCount)
    {
        List<TinTuc> it_r = new List<TinTuc>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("List_NewsBy_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("categoryId", categoryId));
                cmd.Parameters.Add(new SqlParameter("SkipCount", SkipCount));
                cmd.Parameters.Add(new SqlParameter("MaxResultCount", MaxResultCount));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TinTuc it = new TinTuc
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_News_list_AllActive");
                return it_r;
            }
        }
    }
    public static List<TinTuc> list_Lastest_Active(string CategoryId, string Size)
    {
        List<TinTuc> it_r = new List<TinTuc>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_News_listLastestActive1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("Size", Size));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TinTuc it = new TinTuc
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_News_listLastestActive");
                return it_r;
            }
        }
    }

    public static List<TinTuc_Detail> New_Detail(string id)
    {
        List<TinTuc_Detail> it_r = new List<TinTuc_Detail>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_News_Detail1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", id));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TinTuc_Detail it = new TinTuc_Detail
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        NoiDung = reader["NoiDung"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
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

    public static List<TinTuc> list_Related(string Id, string CategoryId, string Size)
    {
        List<TinTuc> it_r = new List<TinTuc>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_News_list_Related1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Id", Id));
                cmd.Parameters.Add(new SqlParameter("CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("Size", Size));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TinTuc it = new TinTuc
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        NgayTao = DateTime.Parse(reader["NgayTao"].ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "S_API_News_listLastestActive");
                return it_r;
            }
        }
    }
    public static List<TinTuc> GetAll_ItemKM_To_Skip(string Tu, string Den)
    {
        List<TinTuc> it_r = new List<TinTuc>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  * FROM  ( SELECT *, ROW_NUMBER() OVER ( ORDER BY inid DESC) AS rn FROM [dbo].[News] ) AS A WHERE A.rn BETWEEN " + Tu + " AND " + Den + "", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TinTuc it = new TinTuc
                    {
                        //inid = Convert.ToInt32(reader["inid"].ToString()),
                        //icid = Convert.ToInt32(reader["icid"].ToString()),
                        //Title = reader["Title"].ToString(),
                        //Brief = reader["Brief"].ToString(),
                        //Images = reader["Images"].ToString(),
                        //ImagesSmall = reader["ImagesSmall"].ToString(),
                        //Create_Date = reader["Create_Date"].ToString(),
                        //Views = Convert.ToInt32(reader["Views"].ToString()),
                        //Contents = reader["Contents"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "GetAll_ItemKM_To_Skip");
                return it_r;
            }
        }
    }

    public static List<TotalRecod> TotalRecod(string Sql)
    {
        List<TotalRecod> it_r = new List<TotalRecod>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(Sql, con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TotalRecod it = new TotalRecod
                    {
                        TotalRecods = reader["Count"].ToString(),
                    };
                    it_r.Add(it);
                }
                con.Close();
                return it_r;
            }
            catch (Exception ex)
            {
                con.Close();
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "TotalRecod");
                return it_r;
            }
        }
    }





}
