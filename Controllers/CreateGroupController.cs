using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
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
    public class CreateGroupController : Controller
    {
        private ApplicationUserManager _userManager;

        public CreateGroupController()
        {
        }

        public CreateGroupController(ApplicationUserManager userManager)
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
            // todo check if user has too many groups

            return View();
        }

        [HttpPost]
        public ActionResult SubmitCreateGroupForm(CreateGroupVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            Guid.TryParse(User.Identity.GetUserId(), out Guid userID);

            if (userID == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            GroupModel group = new GroupModel(formData.Name);

            dbContext.Groups.Add(group);

            dbContext.AppUserGroups.Add(new AppUserGroupModel(userID, group.GroupID, 2)); // 2 = owner

            dbContext.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}