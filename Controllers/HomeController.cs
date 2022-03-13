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
            Purser();
            List<Product> products = entities.Products.Where(temp => temp.Exclusivity.Equals(0)).OrderByDescending(temp=> temp.ProductID).ToList();
            return View(products);

        }

        public ActionResult Purser()
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
            AboutU aboutU = entities.AboutUs.FirstOrDefault();
            return View(aboutU);
        }

        public ActionResult Products()
        {
            List<Product> products = entities.Products.Where(temp => temp.Exclusivity.Equals(0)).ToList();
            return View(products);
        }

        [HttpPost]
        public ActionResult Filter(String filter)
        {
            if (filter != null)
            {
                TempData["filter"] = filter;
            }
            return RedirectToAction("Products","Home");       
        }
        
        [HttpPost]
        public ActionResult Products(String PName)
        {
            List<Product> searchResult = entities.Products.Where(temp => temp.PName.Contains(PName) && temp.Exclusivity.Equals(0)).ToList();
            return View(searchResult);
        }
        public ActionResult Contact()
        {
            if (Session["customerID"] != null)
            {
                int customerID = (int)Session["customerID"];
                Customer customer = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();
                ViewBag.customerInfo = customer.UserName;
                ViewBag.customerEmail = customer.Email;
                ViewBag.customerPhone = customer.Phone;

                AboutU aboutU = entities.AboutUs.FirstOrDefault();

                return View(aboutU);
            }
            else
            {
                AboutU aboutU = entities.AboutUs.FirstOrDefault();

                return View(aboutU);
            }

            
        }

        [HttpPost]
        public ActionResult Contact(String name, String email, String phone, String message)
        {
            Message msg = new Message();
            msg.FullName = name;
            msg.Email = email;
            msg.PhoneNo = phone;
            msg.Message1 = message;
            if(Session["customerID"] != null)
            {
                msg.CustomerID = (int)Session["customerID"];
            }
            entities.Messages.Add(msg);
            entities.SaveChanges();
            AboutU aboutU = entities.AboutUs.FirstOrDefault();
            ViewBag.msgSubmit = "Message sent Successfully";
            return View(aboutU);

        }

        [Authorize]
        public ActionResult ExclusiveProducts()
        {
            List<Product> xproducts = entities.Products.Where(temp => temp.Exclusivity.Equals(1)).ToList();
            return View(xproducts);
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
            int customerID = (int)Session["customerID"];
            List<Wishlist> wishlist = entities.Wishlists.Where(x=> x.CustomerID == customerID).ToList();
            return View(wishlist);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddToWishlist(int productID)
        {
            Wishlist wishlist = new Wishlist();
            wishlist.ProductID = productID;
            if(Session["customerID"] != null)
            {
                wishlist.CustomerID = (int)Session["customerID"];
            }
            wishlist.Status = 1;
            entities.Wishlists.Add(wishlist);
            entities.SaveChanges();

            ViewBag.added = true;

            return RedirectToAction("ProductPreview", new { id = productID });
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveWish(int pID)
        {
            if (Session["customerID"] != null)
            {
                int customerID = (int)Session["customerID"];
                Wishlist wishlists = entities.Wishlists.Where(x => x.ProductID == pID && x.CustomerID == customerID).FirstOrDefault();
                entities.Wishlists.Remove(wishlists);
                entities.SaveChanges();
            }

            return RedirectToAction("ProductPreview", new { id = pID });
        }


        [Authorize]
        [HttpPost]
        public ActionResult RemoveWish2(int productID)
        {
            if (Session["customerID"] != null)
            {
                int customerID = (int)Session["customerID"];
                Wishlist wishlists = entities.Wishlists.Where(x => x.ProductID == productID && x.CustomerID == customerID).FirstOrDefault();
                entities.Wishlists.Remove(wishlists);
                entities.SaveChanges();
                return RedirectToAction("Wishlist", "Home");
            }

            return RedirectToAction("Wishlist", "Home");
        }
        public ActionResult Team()
        {

            return View();
        }

        /************************************************************************************************************************************/

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
            return RedirectToAction("SignUpIn", "Membership");
        }


        /************************************************************************************************************************/
    
        public ActionResult ProductPreview(int id)
        {
            
            var pData = entities.Products.Where(temp=> temp.ProductID == id).FirstOrDefault();
            var pType = pData.PType;
            List<Product> products = entities.Products.Where(temp => temp.PType.Equals(pType) && temp.Exclusivity.Equals(0)).ToList();
            ViewBag.ProductList = products;

            if(Session["customerID"] != null)
            {
                int customerID = (int)Session["customerID"];
                Wishlist wishlists = entities.Wishlists.Where(x => x.ProductID == id && x.CustomerID == customerID).FirstOrDefault();
                if (wishlists != null)
                {
                    ViewBag.ifAdded = true;
                }
                else
                {
                    ViewBag.ifAdded = false;
                }
                
            }
            return View(pData);
        }
    
    
    
    
    }
}