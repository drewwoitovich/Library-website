﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.Controllers
{
    public class HomeController : MasterController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["libraryConnection"].ConnectionString;
        private IBookSqlDAO bookDAO;
        private IUserSqlDAO userDAO;
        private IForumPostSqlDAO forumDAO;

        public HomeController(IUserSqlDAO userDAO, IForumPostSqlDAO forumDAO) : base(userDAO)
        {
            this.forumDAO = forumDAO;
        }

        public ActionResult Index()
        {
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
        public ActionResult SearchResults(Search s)
        {
            if (!s.NewSearch)
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

        public ActionResult ForumPosts()
        {
            var messages = forumDAO.GetAllForumPosts();

            return View("ForumPosts", messages);
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}