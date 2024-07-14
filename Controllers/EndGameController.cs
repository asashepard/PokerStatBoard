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

            foreach (PokerGameModel model in dbContext.PokerGames)
            {
                if (model.EndDateTime > DateTime.Now) // current game
                {
                    model.EndDateTime = DateTime.Now;

                    dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}