using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    public class ItemBanner
    {
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public int id { get; set; }
    }
    public class RootBanner
    {
        public List<ItemBanner> items { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }
}