using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class JoinPlayerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitJoinPlayerForm(JoinPlayerVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            bool success = Guid.TryParse(formData.Name, out var playerID);

            if (!success) // Can't join player
            {
                return RedirectToAction("Game", "Home");
            }

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

            BuyInModel model = new BuyInModel(dbContext.CurrentGame.FirstOrDefault().PokerGameID, playerID, formData.Amount);

            dbContext.BuyIns.Add(model);

            dbContext.Players.FirstOrDefault(p => p.PlayerID == playerID).IsPlaying = true;

            dbContext.SaveChanges();

            return RedirectToAction("Game", "Home");
        }
    }
}