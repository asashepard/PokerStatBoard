﻿using Microsoft.SqlServer.Server;
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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitAddPlayerForm(AddPlayerVM formData)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            PlayerModel model = new PlayerModel(formData.Name);

            dbContext.Players.Add(model);

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}