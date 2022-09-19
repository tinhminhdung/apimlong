using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class QuestionsDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static List<Questions> list_All_Active(string SkipCount, string MaxResultCount)
    {
        List<Questions> it_r = new List<Questions>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("S_API_Questions_list_AllActive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("SkipCount", SkipCount));
                cmd.Parameters.Add(new SqlParameter("MaxResultCount", MaxResultCount));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Questions it = new Questions
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Tieude = reader["Tieude"].ToString(),
                        Mota = reader["Mota"].ToString(),
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



}
