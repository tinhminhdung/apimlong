using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    public class API_News
    {
        public int inid { get; set; }
        public int icid { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Images { get; set; }
        public string ImagesSmall { get; set; }
        public string Create_Date;
        public int Views { get; set; }
        public string Contents { get; set; }
    }

    public class TinTuc
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int id { get; set; }
    }

    public class ResultTinTuc
    {
        public List<TinTuc> items { get; set; }
        public int totalRecord { get; set; }
    }
    public class Root
    {
        public ResultTinTuc result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }



    public class TinTuc_Detail
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string NoiDung { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int id { get; set; }
    }
    public class ResultTinTuc_DT
    {
        public List<TinTuc_Detail> items { get; set; }
    }
    public class RootDT
    {
        public ResultTinTuc_DT result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    public class Record
    {
        public int totalRecord { get; set; }
    }

}