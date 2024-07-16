using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.SqlServer.Server;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Controllers
{
    public class AddPlayerController : Controller
    {
        private ApplicationUserManager _userManager;

        public AddPlayerController()
        {
        }

        public AddPlayerController(ApplicationUserManager userManager)
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
        public ActionResult SubmitAddPlayerForm(AddPlayerVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            foreach (PlayerModel player in dbContext.Players)
            {
                if (player.Name == formData.Name) // Name already exists
                {
                    return RedirectToAction("Game", "Home");
                }
            }

            PlayerModel model = new PlayerModel(formData.Name);

            dbContext.Players.Add(model);

            dbContext.SaveChanges();

            return RedirectToAction("Game", "Home");
        }
    }
}