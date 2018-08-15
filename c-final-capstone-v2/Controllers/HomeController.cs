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

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchResults(Search s, bool newSearch)
        {
            if (!newSearch)
            {
                List<Book> results = bookDAO.MasterSearch(s.TitleSearchInput, s.AuthorSearchInput, s.GenreSearchInput);
                return View("SearchResults", results);
            }
            // Update user last search date
            else
            {
                List<Book> results = bookDAO.MasterSearchNewBooks(s.DateTimeSearchInput, s.TitleSearchInput, s.AuthorSearchInput, s.GenreSearchInput);
                return View("SearchResults", results);
            }
        }

        // Get Form for adding a book
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book b)
        {
            if (!ModelState.IsValid)
            {
                return View("AddBook", b);
            }
            bookDAO.AddBook(b);
            return RedirectToAction("Search");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser lu)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", lu);
            }
            userDAO.UserLogin(lu.Username, lu.Password);
            return RedirectToAction("UserProfile");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUser ru)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", ru);
            }
            userDAO.CreateUser(ru);
            return RedirectToAction("UserProfile");
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}