using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    [Authorize]
    public class StartGameController : Controller
    {
        private ApplicationUserManager _userManager;

        public StartGameController()
        {
        }

        public StartGameController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            if (user == null)
            {
                return RedirectToAction("Game", "Home");
            }

            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault() == null) // populate CurrentGame if necessary
            {
                CurrentGameModel current_model = new CurrentGameModel();
                dbContext.CurrentGame.Add(current_model);
                dbContext.SaveChanges();
            }

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID != Guid.Empty) // CURRENT GAME - redirect to game page for ongoing game
            {
                return RedirectToAction("Game", "Home");
            }

            if (user.accessLevel < 1)
            {
                return RedirectToAction("Index", "NoPermission");
            }

            PokerGameModel model = new PokerGameModel();

            dbContext.PokerGames.Add(model);
            dbContext.CurrentGame.FirstOrDefault().PokerGameID = model.PokerGameID;

            dbContext.SaveChanges();

            return RedirectToAction("Game", "Home");
        }
    }
}