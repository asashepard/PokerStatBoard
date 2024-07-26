using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class LeaderboardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard", "Home");
        }

        [Authorize]
        public ActionResult GroupName(string groupName)
        {
            GroupModel model = GeneralLogic.getGroup(groupName);

            if (model == null) // bad link
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (GeneralLogic.getCurrentGame(model.GroupID).PokerGameID == Guid.Empty) // NO GAME - redirect to home page
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return View(model);
        }
    }
}