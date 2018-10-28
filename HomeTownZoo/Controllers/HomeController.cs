using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeTownZoo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // allows certain groups access actions with identity
        // [Authorize(Roles = "Admin" )]
        public ActionResult About()
        {
            ViewBag.Message = "About Hometown Zoo";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Hometown Zoo";

            return View();
        }
    }
}