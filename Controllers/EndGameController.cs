using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class EndGameController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault() == null) // populate CurrentGame if necessary
            {
                CurrentGameModel current_model = new CurrentGameModel();
                dbContext.CurrentGame.Add(current_model);
                dbContext.SaveChanges();
            }

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO CURRENT GAME - redirect to home page
            {
                return RedirectToAction("Index", "Home");
            }

            PokerGameModel model = dbContext.PokerGames.FirstOrDefault(m => m.PokerGameID.Equals(dbContext.CurrentGame.FirstOrDefault().PokerGameID));

            if (model == null) // ERROR - game not found from CurrentGame entry, redirect to home
            {
                return RedirectToAction("Index", "Home");
            }

            if (GeneralLogic.getCurrentPlayers().Count() > 0) // Players still left at the table that need cashing out
            {
                return RedirectToAction("Index", "CashOutPlayer");
            }

            model.EndDateTime = DateTime.Now;
            dbContext.CurrentGame.FirstOrDefault().PokerGameID = Guid.Empty;

            foreach (PlayerModel player in dbContext.Players)
            {
                if (player.IsPlaying)
                {
                    player.IsPlaying = false;
                }
            }

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}