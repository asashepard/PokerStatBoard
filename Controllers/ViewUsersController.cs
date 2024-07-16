using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PokerStatBoard.Controllers
{
    public class ViewUsersController : Controller
    {
        private ApplicationUserManager _userManager;

        public ViewUsersController()
        {
        }

        public ViewUsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            if (user == null)
            {
                return Content("not found");
            }

            int access = user.accessLevel;

            if (!(user.accessLevel >= 2))
            {
                return RedirectToAction("Index", "Home");
            }

            List<ApplicationUser> users = PokerStatBoard.Logic.GeneralLogic.GetApplicationUsers();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccessLevels(List<ApplicationUser> users)
        {
            if (users == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var user in users)
            {
                var dbUser = UserManager.FindById(user.Id);
                if (dbUser != null)
                {
                    dbUser.accessLevel = user.accessLevel;
                    UserManager.Update(dbUser);
                }
            }

            return RedirectToAction("Index");
        }
    }
}