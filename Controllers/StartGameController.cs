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

            PokerGameModel model = new PokerGameModel();

            dbContext.PokerGames.Add(model);

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}