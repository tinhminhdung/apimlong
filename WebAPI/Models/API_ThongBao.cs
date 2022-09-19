using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    public class Obj_GetCountWaiting
    {
        public double Counts { get; set; }
    }
    public class Root_GetCountWaiting
    {
        public double Counts { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    public class DSThongBao
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int id { get; set; }
        public int DaDoc { get; set; }
        
    }
  
    public class ResultTB
    {
        public List<DSThongBao> items { get; set; }
        public int totalRecord { get; set; }
    }
    public class RootTB
    {
        public ResultTB result { get; set; }
       
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }



    public class TB_Detail
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string NoiDung { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int id { get; set; }
    }
    public class ResultTB_DT
    {
        public List<TB_Detail> items { get; set; }
    }
    public class RootTBDT
    {
        public ResultTB_DT result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    public class CHThongBao
    {
        #region[Entity Private]
        private int _ID;
        private string _Title;
        private string _Brief;
        private string _Contents;
        private string _Keywords;
        private string _search;
        private string _Images;
        private int _Equals;
        private int _Chekdata;
        private DateTime _Create_Date;
        private DateTime _Modified_Date;
        private int _Views;
        private string _lang;
        private int _Status;
        private string _Titleseo;
        private string _Meta;
        private string _Keyword;
        private string _TangName;
        #endregion

        #region[Properties]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Brief { get { return _Brief; } set { _Brief = value; } }
        public string Contents { get { return _Contents; } set { _Contents = value; } }
        public string Keywords { get { return _Keywords; } set { _Keywords = value; } }
        public string search { get { return _search; } set { _search = value; } }
        public string Images { get { return _Images; } set { _Images = value; } }
        public int Equals { get { return _Equals; } set { _Equals = value; } }
        public int Chekdata { get { return _Chekdata; } set { _Chekdata = value; } }
        public DateTime Create_Date { get { return _Create_Date; } set { _Create_Date = value; } }
        public DateTime Modified_Date { get { return _Modified_Date; } set { _Modified_Date = value; } }
        public int Views { get { return _Views; } set { _Views = value; } }
        public string lang { get { return _lang; } set { _lang = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public string Titleseo { get { return _Titleseo; } set { _Titleseo = value; } }
        public string Meta { get { return _Meta; } set { _Meta = value; } }
        public string Keyword { get { return _Keyword; } set { _Keyword = value; } }
        public string TangName { get { return _TangName; } set { _TangName = value; } }
        #endregion

    }


}