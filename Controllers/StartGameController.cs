using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.SqlServer.Server;
using PokerStatBoard.Logic;
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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GroupName(string groupName)
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            if (user == null)
            {
                return RedirectToAction(groupName, "Game");
            }

            ApplicationDbContext dbContext = new ApplicationDbContext();

            GroupModel group = GeneralLogic.getGroup(groupName);

            if (group == null)
            {
                return RedirectToAction(groupName, "Game");
            }

            if (group.PokerGameID != Guid.Empty) // CURRENT GAME - redirect to game page for ongoing game
            {
                return RedirectToAction(groupName, "Game");
            }

            if (user.accessLevel < 1)
            {
                return RedirectToAction("Index", "NoPermission");
            }

            PokerGameModel model = new PokerGameModel();

            dbContext.PokerGames.Add(model);
            dbContext.Groups.FirstOrDefault(g => g.GroupID == group.GroupID).PokerGameID = model.PokerGameID;

            dbContext.SaveChanges();

            return RedirectToAction(groupName, "Game");
        }
    }
}