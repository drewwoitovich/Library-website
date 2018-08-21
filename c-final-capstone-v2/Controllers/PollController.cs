using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.Controllers
{
    public class PollController : MasterController
    {
        private readonly IPollSqlDAO pollDAO;
        private readonly IUserSqlDAO userDAO;

        public PollController(IPollSqlDAO pollDAO, IUserSqlDAO userDAO) : base(userDAO)
        {
            this.pollDAO = pollDAO;
            this.userDAO = userDAO;
        }

        [HttpGet]
        public ActionResult CreatePoll()
        {
            if (base.IsAdmin)
            {
                return View("CreatePoll");
            }
            var model = new LoginUser();
            return RedirectToAction("Login", "User", model);
        }

        [HttpPost]
        public ActionResult CreatePoll(Poll poll)
        {
            if (base.IsAuthenticated)
            {
                poll.Username = CurrentUser;
                pollDAO.CreatePoll(poll);
                return RedirectToAction("PollView", "Poll", new { username = base.CurrentUser });
            }
            var model = new LoginUser();
            return RedirectToAction("Login", "User", model);
        }
    }
}