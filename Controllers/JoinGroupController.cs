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
    [Authorize]
    public class JoinGroupController : Controller
    {
        private ApplicationUserManager _userManager;

        public JoinGroupController()
        {
        }

        public JoinGroupController(ApplicationUserManager userManager)
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

        public ActionResult GroupId(string groupId)
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

            Guid.TryParse(groupId, out Guid groupID);

            if (groupID == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            GroupModel group = GeneralLogic.getGroup(groupID);

            if (group == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            ApplicationDbContext dbContext = new ApplicationDbContext();

            foreach (AppUserGroupModel appUserGroupModel in dbContext.AppUserGroups) // check if user is already in group
            {
                if (appUserGroupModel.ApplicationUserID == id && appUserGroupModel.GroupID == group.GroupID)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }

            AppUserGroupModel appUserGroup = new AppUserGroupModel(id, group.GroupID, 0);

            dbContext.AppUserGroups.Add(appUserGroup);

            dbContext.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}
