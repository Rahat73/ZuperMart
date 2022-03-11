using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zuper_Mart.Models;

namespace Zuper_Mart.Controllers
{
    public class MembershipController : Controller
    {
        // GET: Membership
        ZuperMartEntities entities = new ZuperMartEntities();

        // GET: Membership
        public ActionResult SignUpIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Customer customer)
        {
            if (entities.Customers.Any(temp => temp.Email == customer.Email))
            {
                ViewBag.msg = "Account already exists with this email.";
                return View("~/Views/Membership/SignUpIn.cshtml");
            }
            else
            {
                entities.Customers.Add(customer);
                entities.SaveChanges();
                ViewBag.msg2 = "Account has been created successfully";
                return View("~/Views/Membership/SignUpIn.cshtml");
            }
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(String Email, String Password)
        {
            bool userExist = entities.Customers.Any(x => x.Email == Email && x.Password == Password);
            Customer customer = entities.Customers.FirstOrDefault(x => x.Email == Email && x.Password == Password);

            if (userExist)
            {
                if(customer.Status == 1)
                {
                    FormsAuthentication.SetAuthCookie(customer.CustomerID.ToString(), false);
                    TempData["customer"] = customer;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.msg4 = "User not allowed";
                    return View("~/Views/Membership/SignUpIn.cshtml");
                }
            }

            ViewBag.msg3 = "Email or password is wrong";
            return View("~/Views/Membership/SignUpIn.cshtml");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}