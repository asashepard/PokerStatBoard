using PokerStatBoard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
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

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO GAME - return
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

        public static List<BuyInModel> getCurrentBuyIns()
        {
            List<BuyInModel> buyInModels = new List<BuyInModel>();

            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO GAME - return
            {
                return buyInModels;
            }

            Guid currentGame = dbContext.CurrentGame.FirstOrDefault().PokerGameID;

            foreach (BuyInModel buyIn in dbContext.BuyIns)
            {
                if (buyIn.PokerGameID != currentGame)
                {
                    continue;
                }
                buyInModels.Add(buyIn);
            }

            buyInModels.Sort((BuyInModel a, BuyInModel b) =>
            {
                return b.DateTime.CompareTo(a.DateTime);
            });

            return buyInModels;
        }

        public static List<CashOutModel> getCurrentCashOuts()
        {
            List<CashOutModel> cashOutModels = new List<CashOutModel>();

            ApplicationDbContext dbContext = new ApplicationDbContext();

            if (dbContext.CurrentGame.FirstOrDefault().PokerGameID == Guid.Empty) // NO GAME - return
            {
                return cashOutModels;
            }

            Guid currentGame = dbContext.CurrentGame.FirstOrDefault().PokerGameID;

            foreach (CashOutModel cashOut in dbContext.CashOuts)
            {
                if (cashOut.PokerGameID != currentGame)
                {
                    continue;
                }
                cashOutModels.Add(cashOut);
            }

            cashOutModels.Sort((CashOutModel a, CashOutModel b) =>
            {
                return b.DateTime.CompareTo(a.DateTime);
            });

            return cashOutModels;
        }

        public static PokerGameModel getCurrentGame()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            return context.PokerGames.FirstOrDefault(g => g.PokerGameID == context.CurrentGame.FirstOrDefault().PokerGameID);
        }

        public static PlayerModel getPlayer(Guid playerID)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            return context.Players.FirstOrDefault(p => p.PlayerID == playerID);
        }
    }
}