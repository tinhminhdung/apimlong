using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
   
    public class DSKhuyenMai
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int id { get; set; }
    }
  
    public class ResultKM
    {
        public List<DSKhuyenMai> items { get; set; }
        public int totalRecord { get; set; }
    }
    public class RootKM
    {
        public ResultKM result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }



    public class KM_Detail
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string NoiDung { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int id { get; set; }
    }
    public class ResultKM_DT
    {
        public List<KM_Detail> items { get; set; }
    }
    public class RootKMDT
    {
        public ResultKM_DT result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }
}