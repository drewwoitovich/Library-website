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

        public ActionResult Poll()
        {
            var pollView = new PollViewModel();
            pollView.CreatePoll = new CreatePollModel();
            pollView.PollResults = pollDAO.GetPollResults();
            ViewBag.BookTitles = pollDAO.ListAllBooks();
            ViewBag.Authors = pollDAO.ListAllAuthors();

            if (base.IsAuthenticated)
            {
                return View("PollView", pollView);
            }
            var model = new LoginUser();
            return RedirectToAction("Login", "User", model);
        }

        [HttpPost]
        public ActionResult Poll(CreatePollModel poll)
        {
            if (base.IsAuthenticated)
            {
                poll.Username = CurrentUser;
                pollDAO.CreatePoll(poll);
                return RedirectToAction("Poll", "Poll", new { username = base.CurrentUser });
            }
            var model = new LoginUser();
            return RedirectToAction("Login", "User", model);
        }
    }
}