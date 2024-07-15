using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Logic
{
    public class GeneralLogic
    {
        public static decimal getAmountOnTable()
        {
            decimal amount = 0;

            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO GAME - no money on table, return 0
            {
                return 0;
            }

            Guid currentGame = dbContext.CurrentGame.FirstOrDefault().PokerGameID;

            foreach(BuyInModel buyIn in dbContext.BuyIns)
            {
                if (buyIn.PokerGameID != currentGame)
                {
                    continue;
                }
                amount += buyIn.Amount;
            }

            foreach (CashOutModel cashOut in dbContext.CashOuts)
            {
                if (cashOut.PokerGameID != currentGame)
                {
                    continue;
                }
                amount -= cashOut.Amount;
            }

            return amount;
        }

        public static List<PlayerModel> getCurrentPlayers()
        {
            List<PlayerModel> players = new List<PlayerModel>();

            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO GAME - return null
            {
                return players;
            }

            Guid currentGame = dbContext.CurrentGame.FirstOrDefault().PokerGameID;

            foreach (PlayerModel player in dbContext.Players)
            {
                if (player.IsPlaying)
                {
                    players.Add(player);
                }
            }

            return players;
        }

        public static decimal getCurrentIn(PlayerModel player)
        {
            decimal amount = 0;

            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO GAME - return 0
            {
                return amount;
            }

            Guid currentGame = dbContext.CurrentGame.FirstOrDefault().PokerGameID;

            foreach (BuyInModel buyIn in dbContext.BuyIns)
            {
                if (buyIn.PokerGameID != currentGame || buyIn.PlayerID != player.PlayerID)
                {
                    continue;
                }
                amount += buyIn.Amount;
            }

            foreach (CashOutModel cashOut in dbContext.CashOuts)
            {
                if (cashOut.PokerGameID != currentGame || cashOut.PlayerID != player.PlayerID)
                {
                    continue;
                }
                amount -= cashOut.Amount;
            }

            return amount;
        }
    }
}