using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
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
        private ApplicationUserManager _userManager;

        public EndGameController()
        {
        }

        public EndGameController(ApplicationUserManager userManager)
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
                return RedirectToAction("Dashboard", "Home");
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

            ApplicationDbContext dbContext = new ApplicationDbContext();

            GroupModel group = GeneralLogic.getGroup(groupName);

            if (group == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (group.PokerGameID == Guid.Empty) // NO CURRENT GAME - redirect to home page
            {
                return RedirectToAction("Dashboard", "Home");
            }

            PokerGameModel model = GeneralLogic.getCurrentGame(group.GroupID);

            if (model == null) // ERROR - game not found, redirect to home
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (GeneralLogic.getCurrentPlayers(group.GroupID).Count() > 0) // Players still left at the table that need cashing out
            {
                return RedirectToAction("GroupName", "CashOutPlayer", new { groupName = group.Name });
            }

            dbContext.PokerGames.FirstOrDefault(x => x.PokerGameID == model.PokerGameID).EndDateTime = DateTime.Now;
            dbContext.Groups.FirstOrDefault(g => g.GroupID == group.GroupID).PokerGameID = Guid.Empty;

            foreach (PlayerModel player in dbContext.Players)
            {
                if (player.IsPlaying && player.GroupID == group.GroupID)
                {
                    player.IsPlaying = false;
                }
            }

            dbContext.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}