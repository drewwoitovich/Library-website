using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.Controllers
{
    public class ForumController : MasterController
    {

        private readonly IForumPostSqlDAO forumDAO;

        public ForumController(IForumPostSqlDAO forumDAO, IUserSqlDAO userDAO) :
            base(userDAO)
        {
            this.forumDAO = forumDAO;
        }

        [Route("users/{username}/dashboard")]
        public ActionResult Dashboard(string username)
        {
            var conversations = forumDAO.GetAllForumPosts();
            return View("Dashboard", conversations);
        }

        // GET: Forum
        public ActionResult ForumPosts()
        {
            var conversations = forumDAO.GetAllForumPosts();
            return View("Dashboard", conversations);
        }

        public ActionResult Create()
        {
            return View("Create");   
        }

        [HttpPost]
        public ActionResult Create(ForumPost post)
        {
            if (base.IsAuthenticated)
            {
                post.Username = CurrentUser;
                forumDAO.CreatePost(post);
                return RedirectToAction("Dashboard", "Forum", new { username = base.CurrentUser });
            }
            var model = new LoginUser();
            return View("Login", "User", model);
        }
    }
}