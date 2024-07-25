using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System.Web;
using System.Web.Mvc;
using PokerStatBoard.Logic;
using System;

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

            GroupModel group = GeneralLogic.getGroup(groupName);

            if (group == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            AddPlayerVM model = new AddPlayerVM
            {
                GroupName = group.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAddPlayerForm(AddPlayerVM formData)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                GroupModel group = GeneralLogic.getGroup(formData.GroupName);

                if (group == null)
                {
                    return RedirectToAction("Game", "Home");
                }

                PlayerModel model = new PlayerModel(formData.Name, group.GroupID);
                dbContext.Players.Add(model);
                dbContext.SaveChanges();
            }

            return RedirectToAction("GroupName", "Game", new { groupName = formData.GroupName });
        }
    }
}
