using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]//Remove if not working
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(USER U)
        {
            Inventory_Management_SystemEntities obj = new Inventory_Management_SystemEntities();
            var data = obj.st_getLoginDetails(U.User_Name, U.Password);
            foreach (var item in data)
            {
                if (Session["id"] == null)
                {
                    if (item.Username == U.User_Name && item.Password == U.Password)
                    {
                        string r = obj.st_getRoleWRTuser(U.User_Name).Single();
                        Session["role"] = r;
                        Session["name"] = U.User_Name;
                        Session["id"] = item.id;
                        //LoggedInUser lod = new LoggedInUser(U.ID);
                        return RedirectToAction("Main");
                    }
                    else
                    {

                    }
                }
               

            }
            return View();
        }
        public ActionResult Register()
        {

               return View();
           
        }

        public ActionResult Logout()
        {
            Session.Remove("name");
            Session.Remove("role");
            return View("Index");
        }

        public ActionResult Main()
        {
            if(Session["name"]==null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            { 
            return View();
            }
        }

    }
}