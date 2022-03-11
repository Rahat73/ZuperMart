using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zuper_Mart.Models;

namespace Zuper_Mart.Controllers
{
    public class HomeController : Controller
    {
        ZuperMartEntities entities = new ZuperMartEntities();


        
        public ActionResult Index()
        {
            if(TempData["customer"] == null)
            {
                return View();
            }
            else
            {
                Customer customerInfo = TempData["customer"] as Customer;
                int customerID = customerInfo.CustomerID;
                Customer customers = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();
                ViewBag.username = customers.UserName;
                Session["customerID"] = customerID;
                return View();
            }
            
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
            /*Customer customerInfo = TempData["customer"] as Customer;*/
            int customerID = (int)Session["customerID"];
            Customer customers = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();
            return View(customers);
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

        [Authorize]
        [HttpPost]
        public ActionResult Update(String ImageURL, String UserName, String Phone, String Address)
        {
            int customerID = (int)Session["customerID"];
            Customer customer = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();

            
            //ImageURL = "~/images/" + ImageURL;
            //filename = Path.Combine(Server.MapPath("~/images/"), filename);
            
            if(customer != null)
            {
                customer.ImageURL = ImageURL;
                customer.UserName = UserName;
                customer.Phone = Phone;
                customer.Address = Address;

                entities.SaveChanges();
                TempData["updated"] = "Profile updated successfully";
                return RedirectToAction("UserProfile", "Home");
            }

            return RedirectToAction("UserProfile", "Home");

        }

        [Authorize]
        [HttpPost]
        public ActionResult ResetPassword(String RegisterPassword)
        {
            int customerID = (int)Session["customerID"];
            Customer customer = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();

            customer.Password = RegisterPassword;
            entities.SaveChanges();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}