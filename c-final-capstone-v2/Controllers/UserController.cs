using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using c_final_capstone_v2.DAL;
using c_final_capstone_v2.Models;

namespace c_final_capstone_v2.Controllers
{
    public class UserController : MasterController
    {
        private readonly IUserSqlDAO userDAO;

        public UserController(IUserSqlDAO userDAO)
            : base(userDAO)
        {
            this.userDAO = userDAO;
        }

        // ACCOUNT MANAGEMENT ACTIONS

        //[HttpGet]
        //[Route("users/{username}/changepassword")]
        //public ActionResult ChangePassword(string username)
        //{
        //    var model = new ChangePasswordViewModel();
        //    return View("ChangePassword", model);
        //}

        //[HttpPost]
        //[Route("users/{username}/changepassword")]
        //public ActionResult ChangePassword(string username, ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("ChangePassword", model);
        //    }

        //    userDal.ChangePassword(username, model.NewPassword);

        //    return RedirectToAction("Dashboard", "Messages", new { username = base.CurrentUser });
        //}

        [HttpGet]
        [Route("users/new")]
        public ActionResult NewUser()
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Forum", new { username = base.CurrentUser });
            }
            else
            {
                var model = new RegisterUser();
                return View("NewUser", model);
            }
        }

        [HttpPost]
        [Route("users/new")]
        public ActionResult NewUser(RegisterUser model)
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Forum", new { username = base.CurrentUser });
            }

            if (ModelState.IsValid)
            {
                var currentUser = userDAO.GetUser(model.Username);

                if (currentUser != null)
                {
                    ViewBag.ErrorMessage = "This username is unavailable";
                    return View("NewUser", model);
                }

                var newUser = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Newsletter = model.Newsletter,
                    Email = model.Email
                };

                // Add the user to the database
                userDAO.CreateUser(newUser);

                // Log the user in and redirect to the dashboard
                base.LogUserIn(model.Username, false);
                return RedirectToAction("Dashboard", "Forum", new { username = model.Username });
            }
            else
            {
                return View("NewUser", model);
            }
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            if (base.IsAuthenticated)
            { 
                return RedirectToAction("Dashboard", "Forum", new { username = base.CurrentUser });
            }

            var model = new LoginUser();
            return View("Login", model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                var user = userDAO.GetUser(model.Username);

                if (user == null)
                {
                    ModelState.AddModelError("invalid-user", "The username provided does not exist");
                    return View("Login", model);
                }
                else if (user.Password != model.Password)
                {
                    ModelState.AddModelError("invalid-password", "The password provided is not valid");
                    return View("Login", model);

                }

                // Happy Path
                base.LogUserIn(user.Username, user.IsAdmin);

                //If they are supposed to be redirected then redirect them else send them to the dashboard
                var queryString = this.Request.UrlReferrer.Query;
                var urlParams = HttpUtility.ParseQueryString(queryString);
                if (urlParams["landingPage"] != null)
                {
                    // then redirect them
                    return new RedirectResult(urlParams["landingPage"]);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Forum", new { username = user.Username });
                }



                //return RedirectToAction("Dashboard", "Messages", new { username = user.Username });
            }
            else
            {
                return View("Login", model);
            }
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            base.LogUserOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("users/deleteuser")]
        public ActionResult DeleteUser()
        {
            if (base.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [Route("users/deleteuser")]
        public ActionResult DeleteUser(string user)
        {
            if (base.IsAdmin)
            {
                // do the work to delte
                return View("DeleteResponse");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult MyProfile()
        {
            List<List<Book>> readingList = new List<List<Book>>();
            if (base.IsAuthenticated)
            {
                readingList = userDAO.GetReadingList(CurrentUser);
                return View("MyProfile", readingList);
            }

            var model = new LoginUser();
            return View("Login", model);
        }


        [HttpGet]
        [Route("users/AddToReadingList")]
        public ActionResult AddToReadingList(int bookId)
        {
            if (base.IsAuthenticated)
            {
                if (userDAO.CheckReadingListAvailability(CurrentUser, bookId))
                {
                    userDAO.AddToReadingList(CurrentUser, bookId);
                    return RedirectToAction("MyProfile", "User");
                }
                return RedirectToAction("MyProfile", "User");
            }
            var model = new LoginUser();
            return RedirectToAction("Login", "User", model);
        }
    }
}