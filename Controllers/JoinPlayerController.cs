using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokerStatBoard.Logic;

namespace PokerStatBoard.Controllers
{
    public class JoinPlayerController : Controller
    {
        private ApplicationUserManager _userManager;

        public JoinPlayerController()
        {
        }

        public JoinPlayerController(ApplicationUserManager userManager)
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

            Guid.TryParse(userId, out Guid id);

            if (id == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (GeneralLogic.getAccessLevel(id, GeneralLogic.getGroup(groupName).GroupID) < 1)
            {
                return RedirectToAction("Index", "NoPermission");
            }

            GroupModel groupModel = GeneralLogic.getGroup(groupName);

            if (groupModel == null)
            {
                return RedirectToAction("Game", "Home");
            }

            JoinPlayerVM model = new JoinPlayerVM
            {
                Group = groupModel
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitJoinPlayerForm(JoinPlayerVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            bool success = Guid.TryParse(formData.Name, out var playerID);

            if (!success) // Can't join player
            {
                return RedirectToAction("Dashboard", "Home");
            }

            Guid groupID = GeneralLogic.getPlayer(playerID).GroupID;

            PokerGameModel currentGame = GeneralLogic.getCurrentGame(groupID);

            if (currentGame == null) // bad link
            {
                return RedirectToAction("Dashboard", "Home");
            }

            GroupModel group = GeneralLogic.getGroup(groupID);

            BuyInModel model = new BuyInModel(currentGame.PokerGameID, playerID, formData.Amount);

            dbContext.BuyIns.Add(model);

            dbContext.Players.FirstOrDefault(p => p.PlayerID == playerID).IsPlaying = true;

            dbContext.SaveChanges();

            return RedirectToAction("GroupName", "Game", new { groupName = group.Name });
        }
    }
}