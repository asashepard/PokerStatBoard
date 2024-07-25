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
            return RedirectToAction("Game", "Home");
        }

        public ActionResult GroupName(string groupName)
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

            GroupModel groupModel = GeneralLogic.getGroup(groupName);

            if (groupModel == null)
            {
                return RedirectToAction("Game", "Home");
            }

            CashOutPlayerVM model = new CashOutPlayerVM
            {
                Group = groupModel
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitCashOutPlayerForm(CashOutPlayerVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            bool success = Guid.TryParse(formData.Name, out var playerID);

            if (!success) // Can't cash out player
            {
                return RedirectToAction("Dashboard", "Home");
            }

            Guid groupID = GeneralLogic.getPlayer(playerID).GroupID;

            PokerGameModel currentGame = GeneralLogic.getCurrentGame(groupID);

            if (currentGame == null) // bad link
            {
                return RedirectToAction("Dashboard", "Home");
            }

            decimal amount = formData.Amount;

            decimal onTable = GeneralLogic.getAmountOnTable(groupID);

            if (amount > onTable)
            {
                amount = onTable; // prevent cashing out for more than is on table
            }

            if (GeneralLogic.getCurrentPlayers(groupID).Count == 1)
            {
                amount = onTable; // make sure all money is accounted for by giving remainder to last player
            }

            GroupModel group = GeneralLogic.getGroup(groupID);

            CashOutModel model = new CashOutModel(currentGame.PokerGameID, playerID, amount);

            dbContext.CashOuts.Add(model);

            dbContext.Players.FirstOrDefault(p => p.PlayerID == playerID).IsPlaying = false;

            dbContext.SaveChanges();

            return RedirectToAction("GroupName", "Game", new { groupName = group.Name });
        }
    }
}