using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NRL_Ladder_FrontEnd_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "NRL Ladder Application";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "NRL ladder Contact Details";

            return View();
        }
    }
}