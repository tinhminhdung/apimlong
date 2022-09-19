using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class BannerDataAccess
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static List<ItemBanner> Get_List_Home_Banners(string id)
    {
        List<ItemBanner> it_r = new List<ItemBanner>();
        using (var con = new SqlConnection(strConn))
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT images as id ,Path as Link ,'https://minhlongfinance.vn'+ CAST( vimg as nvarchar(MAX))  as ImageUrl FROM [dbo].[Advertisings] where value='" + id + "'  and Status=1  order by Orders asc", con);
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ItemBanner it = new ItemBanner
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        Link = reader["link"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
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

}
