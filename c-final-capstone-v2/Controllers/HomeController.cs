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
            //List<Book> test = bookDAO.SearchByTitle("1984");
            return View("Index");
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

        [HttpPost]
        public ActionResult Search(Search s)
        {
            List<Book> results = bookDAO.MasterSearch(s.TitleSearchInput, s.AuthorSearchInput, s.GenreSearchInput);
            // Update user last search date
            return View("Search", results);
        }


    }
}