using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class PlayerStatsController : Controller
    {
        public ActionResult Index(Guid id)
        {
            var player = GeneralLogic.getPlayer(id);
            if (player == null)
            {
                return RedirectToAction("Leaderboard", "Home");
            }
            return View(player);
        }
    }
}