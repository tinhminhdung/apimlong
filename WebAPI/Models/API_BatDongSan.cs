using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
   
    public class DSBatDongSan
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string NamDuAn { get; set; }
        public string TienDuAn { get; set; }
    }
  
    public class ResultBDS
    {
        public List<DSBatDongSan> items { get; set; }
        public int totalRecord { get; set; }

    }
    public class RootBDS
    {
        public ResultBDS result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }



    public class BDS_Detail
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string NoiDung { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string NamDuAn { get; set; }
        public string TienDuAn { get; set; }
        public int id { get; set; }
    }
    public class ResultBDS_DT
    {
        public List<BDS_Detail> items { get; set; }
    }
    public class RootBDSDT
    {
        public ResultBDS_DT result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }
}