using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.SqlServer.Server;
using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private ApplicationUserManager _userManager;

        public GameController()
        {
        }

        public GameController(ApplicationUserManager userManager)
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
            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult GroupName(string groupName)
        {
            GroupModel model = GeneralLogic.getGroup(groupName);

            if (model == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (model.PokerGameID == Guid.Empty)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return View(model);
        }
    }
}