using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;
using System.Configuration;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.Controllers
{
    public class ForumController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["libraryConnection"].ConnectionString;

        private IForumPostSqlDAO forumDAO;

        public ForumController()
        {
            forumDAO = new ForumPostSqlDAO(connectionString);
        }

        // GET: Forum
        public ActionResult ForumPosts()
        {
            List<ForumPost> allPosts = forumDAO.GetAllFoumPosts();
            return View("ForumPosts", allPosts);
        }
    }
}