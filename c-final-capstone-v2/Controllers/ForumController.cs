using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;

namespace c_final_capstone_v2.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUserSqlDAO userDAO;

        public ForumController(IUserSqlDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }
    }
}