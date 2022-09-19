using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
   
    public class Questions
    {
        public string Tieude { get; set; }
        public string Mota { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int id { get; set; }
    }
  
    public class ResultQuestions
    {
        public List<Questions> items { get; set; }
        public int totalRecord { get; set; }
    }
    public class RootFAQ
    {
        public ResultQuestions result { get; set; }
 
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }



    public class DieuKhoan_Detail
    {
        public string Contens { get; set; }
    }

    public class RootDieuKhoan
    {
        public List<DieuKhoan_Detail> items { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }


}