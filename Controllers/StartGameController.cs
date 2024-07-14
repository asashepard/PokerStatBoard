using Microsoft.SqlServer.Server;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class StartGameController : Controller
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

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID != Guid.Empty) // NO CURRENT GAME - redirect to home page
            {
                return RedirectToAction("Index", "Home");
            }

            PokerGameModel model = new PokerGameModel();

            dbContext.PokerGames.Add(model);
            dbContext.CurrentGame.FirstOrDefault().PokerGameID = model.PokerGameID;

            dbContext.SaveChanges();

            return RedirectToAction("Game", "Home");
        }
    }
}