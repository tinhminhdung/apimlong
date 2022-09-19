using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Response.Redirect("/swagger");
            return View();
        }

        //mở Package Manager Console và run:
        //- Enable-Migrations
        //- Add-Migration
        //- Update-Database

    }
}
