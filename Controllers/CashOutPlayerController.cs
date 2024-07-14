using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class CashOutPlayerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitCashOutPlayerForm(CashOutPlayerVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            bool success = Guid.TryParse(formData.Name, out var playerID);

            if (!success) // Can't cash out player
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

            CashOutModel model = new CashOutModel(dbContext.CurrentGame.FirstOrDefault().PokerGameID, playerID, formData.Amount);

            dbContext.CashOuts.Add(model);

            dbContext.SaveChanges();

            return RedirectToAction("Game", "Home");
        }
    }
}