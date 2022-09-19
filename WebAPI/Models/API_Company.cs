using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    
    public class Company_Detail
    {
        public string TenCongTy { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
    }

    public class RootCompany
    {
        public List<Company_Detail> items { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

}