using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using c_final_capstone_v2.DAL;

namespace c_final_capstone_v2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookSqlDAO bookDAO;
        private readonly IUserSqlDAO userDAO;

        public HomeController(IBookSqlDAO bookDAO, IUserSqlDAO userDAO)
        {
            this.bookDAO = bookDAO;
            
            this.userDAO = userDAO;
        }

        public ActionResult Index()
        {

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
    }
}