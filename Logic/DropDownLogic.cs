using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerStatBoard.Logic
{
    public class DropDownLogic
    {
        public static List<SelectListItem> GetPlayerNames()
        {
            List<SelectListItem> outputList = new List<SelectListItem>();

            ApplicationDbContext dbContext = new ApplicationDbContext();
            List<PlayerModel> players = dbContext.Players.ToList();

            foreach (PlayerModel player in players)
            {
                SelectListItem selectListItem = new SelectListItem();

                selectListItem.Text = player.Name;
                selectListItem.Value = player.PlayerID.ToString();

                outputList.Add(selectListItem);
            }

            return outputList;
        }
    }
}