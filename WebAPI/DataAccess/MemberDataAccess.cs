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
public class SMember
{
    private static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    #region Name Text
    public static List<Member> Name_Text(string Name_Text)
    {
        SqlConnection conn = Database.Connection();
        SqlCommand comm = new SqlCommand(Name_Text, conn);
        comm.CommandType = CommandType.Text;
        try
        {
            return Database.Bind_List_Reader<Member>(comm);
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
    public static List<InfoMenberdb> Name_Procedure(string Name_Text,string IDThanhVien)
    {
        SqlConnection conn = Database.Connection();
        SqlCommand comm = new SqlCommand(Name_Text, conn);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add(new SqlParameter("@IDThanhVien", IDThanhVien));
        try
        {
            return Database.Bind_List_Reader<InfoMenberdb>(comm);
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
    //
    #endregion

    #region GET BY ID
    public static List<Member> GETBYID(string ID)
    {
        SqlConnection conn = Database.Connection();
        SqlCommand comm = new SqlCommand("select * from Members where ID=@ID", conn);
        comm.CommandType = CommandType.Text;

        comm.Parameters.Add(new SqlParameter("@ID", ID));
        try
        {
            return Database.Bind_List_Reader<Member>(comm);
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
}
