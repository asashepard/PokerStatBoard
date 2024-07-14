using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Models
{
    public class BuyInModel
    {
        [Key]
        public Guid BuyInID { get; set; }

        [Required]
        public Guid PokerGameID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public BuyInModel(Guid PokerGameID, decimal Amount)
        {
            BuyInID = Guid.NewGuid();
            this.PokerGameID = PokerGameID;
            this.Amount = Amount;
            DateTime = DateTime.Now;
        }
    }
}