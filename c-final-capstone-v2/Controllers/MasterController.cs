using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;
using System.Web.Routing;

namespace c_final_capstone_v2.Controllers
{
    public class MasterController : Controller
    {
        private const string UsernameKey = "Library_Username";
        private const string IsAdminKey = "Is_Admin";
        private readonly IUserSqlDAO userDAO;

        public MasterController(IUserSqlDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user session exists
                if (Session[UsernameKey] != null)
                {
                    username = (string)Session[UsernameKey];
                }
                return username;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Session[UsernameKey] != null;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return Session[IsAdminKey] != null;
            }
        }

        public void LogUserIn(string username, bool isAdmin)
        {
            Session[UsernameKey] = username;
            if (isAdmin)
            {
                Session[IsAdminKey] = isAdmin;
            }
        }

        public void LogUserOut()
        {
            Session.Abandon();
        }

        [ChildActionOnly]
        public ActionResult GetAuthenticatedUser()
        {
            User model = null;

            if (IsAuthenticated)
            {
                model = userDAO.GetUser(CurrentUser);
            }

            return View("_AuthenticationBar", model);
        }
    }
}