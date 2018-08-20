using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;

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
        public ActionResult Poll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Poll()
        {

        }
    }
}