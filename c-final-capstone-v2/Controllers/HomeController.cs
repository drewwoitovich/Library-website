using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["libraryConnection"].ConnectionString;
        private IBookSqlDAO bookDAO;
        private IUserSqlDAO userDAO;

        public HomeController()
        {
            bookDAO = new BookSqlDAO(connectionString);
            userDAO = new UserSqlDAO(connectionString);
        }

        public ActionResult Index()
        {
            List<Book> test = bookDAO.SearchByTitle("Star");
            return View("Index", test);
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