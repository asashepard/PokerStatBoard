using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PokerStatBoard.Models;
using PokerStatBoard.ViewModels;
using System.Web;
using System.Web.Mvc;
using PokerStatBoard.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerStatBoard.Controllers
{
    public class GroupAccessController : Controller
    {
        private ApplicationUserManager _userManager;

        public GroupAccessController()
        {
        }

        public GroupAccessController (ApplicationUserManager userManager)
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

            return View(GeneralLogic.getAppUserGroupModels(group.GroupID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccessLevels(List<AppUserGroupModel> userGroupModels)
        {
            if (userGroupModels == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            ApplicationDbContext context = new ApplicationDbContext();

            foreach (AppUserGroupModel model in context.AppUserGroups)
            {
                if (!userGroupModels.Contains(model))
                {
                    continue;
                }

                model.AccessLevel = userGroupModels.FirstOrDefault(m => m.ApplicationUserGroupID == model.ApplicationUserGroupID).AccessLevel;
            }

            context.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}
