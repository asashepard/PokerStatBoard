﻿using PokerStatBoard.Logic;
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
            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult PlayerId(Guid playerId)
        {
            if (playerId == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            Guid.TryParse(playerId.ToString(), out Guid playerID);

            if (playerID == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            var player = GeneralLogic.getPlayer(playerID);

            if (player == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return View(player);
        }
    }
}