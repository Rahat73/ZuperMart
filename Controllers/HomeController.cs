using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zuper_Mart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Products()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult ExclusiveProducts()
        {

            return View();
        }

        [Authorize]
        public ActionResult UserProfile()
        {

            return View();
        }

        [Authorize]
        public ActionResult Wishlist()
        {

            return View();
        }

        public ActionResult Team()
        {

            return View();
        }
    }
}