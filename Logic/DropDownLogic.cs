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
        public static List<SelectListItem> GetPlayers(Guid groupID)
        {
            List<SelectListItem> outputList = new List<SelectListItem>();

            ApplicationDbContext dbContext = new ApplicationDbContext();
            List<PlayerModel> players = dbContext.Players.ToList();

            foreach (PlayerModel player in players)
            {
                if (player.GroupID != groupID)
                {
                    continue;
                }

                SelectListItem selectListItem = new SelectListItem();

                selectListItem.Text = player.Name;
                selectListItem.Value = player.PlayerID.ToString();

                outputList.Add(selectListItem);
            }

            return outputList;
        }

        public static List<SelectListItem> GetPlayersCurrentlyPlaying(Guid groupID)
        {
            List<SelectListItem> outputList = new List<SelectListItem>();

            ApplicationDbContext dbContext = new ApplicationDbContext();
            List<BuyInModel> buyIns = dbContext.BuyIns.ToList();
            List<CashOutModel> cashOuts = dbContext.CashOuts.ToList();
            Guid currentGameId = GeneralLogic.getCurrentGame(groupID).PokerGameID;

            if (currentGameId == Guid.Empty)
            {
                return outputList;
            }

            List<PlayerModel> currentPlayers = new List<PlayerModel>();
            foreach (BuyInModel buyIn in buyIns)
            {
                if (buyIn.PokerGameID != currentGameId) // only look at current poker game buy-ins
                {
                    continue;
                }
                PlayerModel player = dbContext.Players.FirstOrDefault(p => p.PlayerID == buyIn.PlayerID);
                if (!currentPlayers.Contains(player))
                {
                    currentPlayers.Add(player);
                }
            }

            foreach (CashOutModel cashOut in cashOuts)
            {
                if (cashOut.PokerGameID != currentGameId) // only look at current poker game cash-outs
                {
                    continue;
                }
                PlayerModel player = dbContext.Players.FirstOrDefault(p => p.PlayerID == cashOut.PlayerID);
                if (currentPlayers.Contains(player))
                {
                    currentPlayers.Remove(player);
                }
            }

            foreach (PlayerModel player in currentPlayers)
            {
                SelectListItem selectListItem = new SelectListItem();

                selectListItem.Text = player.Name;
                selectListItem.Value = player.PlayerID.ToString();

                outputList.Add(selectListItem);
            }

            return outputList;
        }

        public static List<SelectListItem> GetGroups(Guid userID)
        {
            List<SelectListItem> outputList = new List<SelectListItem>();

            ApplicationDbContext dbContext = new ApplicationDbContext();
            List<BuyInModel> buyIns = dbContext.BuyIns.ToList();
            List<CashOutModel> cashOuts = dbContext.CashOuts.ToList();

            List<GroupModel> groups = GeneralLogic.getGroups(userID);

            foreach (GroupModel g in groups)
            {
                SelectListItem selectListItem = new SelectListItem();

                selectListItem.Text = g.Name;
                selectListItem.Value = g.GroupID.ToString();

                outputList.Add(selectListItem);
            }

            return outputList;
        }
        public static List<SelectListItem> GetGroupAccessLevels(int to)
        {
            List<SelectListItem> outputList = new List<SelectListItem>();

            for (int i = 1; i <= to; i++)
            {
                SelectListItem selectListItem = new SelectListItem();

                selectListItem.Text = GeneralLogic.accessLevelToString(i);
                selectListItem.Value = i.ToString();

                outputList.Add(selectListItem);
            }

            return outputList;
        }
    }
}