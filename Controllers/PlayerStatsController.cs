using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    [Authorize]
    public class PlayerStatsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Leaderboard", "Home");
        }

        public ActionResult Name(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Leaderboard", "Home");
            }
            var player = GeneralLogic.getPlayer(name);
            if (player == null)
            {
                return RedirectToAction("Leaderboard", "Home");
            }
            return View(player);
        }
    }
}