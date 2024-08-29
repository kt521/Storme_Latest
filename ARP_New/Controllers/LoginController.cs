using ARP_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARP_New.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin(clsLogins cls)
        {
            try
            {

                cls = cls.Login(cls);
                Session["Firstname"] = cls.Firstname;
                Session["Lastname"] = cls.Lastname;

                return Json(cls, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw new Exception("Error in User Register");
            }
        }
    }
}