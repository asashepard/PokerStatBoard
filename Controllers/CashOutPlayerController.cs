using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PokerStatBoard.Logic;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class CashOutPlayerController : Controller
    {
        private ApplicationUserManager _userManager;

        public CashOutPlayerController()
        {
        }

        public CashOutPlayerController(ApplicationUserManager userManager)
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

            if (user.accessLevel < 1)
            {
                return RedirectToAction("Index", "NoPermission");
            }

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

            decimal amount = formData.Amount;

            decimal onTable = GeneralLogic.getAmountOnTable();

            if (amount > onTable)
            {
                amount = onTable; // prevent cashing out for more than is on table
            }

            if (GeneralLogic.getCurrentPlayers().Count == 1)
            {
                amount = onTable; // make sure all money is accounted for by giving remainder to last player
            }

            CashOutModel model = new CashOutModel(dbContext.CurrentGame.FirstOrDefault().PokerGameID, playerID, amount);

            dbContext.CashOuts.Add(model);

            dbContext.Players.FirstOrDefault(p => p.PlayerID == playerID).IsPlaying = false;

            dbContext.SaveChanges();

            return RedirectToAction("Game", "Home");
        }
    }
}