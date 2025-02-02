using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString;

        public HomeController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
        }
        // GET: Home
        public ActionResult Index()
        {
            int userId = (int)Session["UserID"];
            ViewBag.UserID = userId;

            UserBuislayer userBuislayer = new UserBuislayer(connectionString);
            int roleID = userBuislayer.GetRoleByUserID(userId);
            ViewBag.RoleID = roleID;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Your Error page.";

            return View();
        }
    }
}