using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager)
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
            return View();
        }

        [Authorize]
        public ActionResult Game(string groupName)
        {
            GroupModel model = GeneralLogic.getGroup(groupName);

            if (model == null) // bad link
            {
                return RedirectToAction("Index", "Home");
            }

            if (GeneralLogic.getCurrentGame(model.GroupID).PokerGameID == Guid.Empty) // NO GAME - redirect to home page
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            Guid.TryParse(User.Identity.GetUserId(), out Guid userID);

            if (userID == null)
            {
                return View("Index", "Home");
            }

            return View(GeneralLogic.getGroups(userID));
        }

        [Authorize]
        public ActionResult Leaderboard(string groupName)
        {
            GroupModel model = GeneralLogic.getGroup(groupName);

            if (model == null) // bad link
            {
                return RedirectToAction("Index", "Home");
            }

            if (GeneralLogic.getCurrentGame(model.GroupID).PokerGameID == Guid.Empty) // NO GAME - redirect to home page
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}