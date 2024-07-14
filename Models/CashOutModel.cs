using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Models
{
    public class CashOutModel
    {
        [Key]
        public Guid CashOutID { get; set; }

        [Required]
        public Guid PokerGameID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public CashOutModel(Guid PokerGameID, decimal Amount)
        {
            CashOutID = Guid.NewGuid();
            this.PokerGameID = PokerGameID;
            this.Amount = Amount;
            DateTime = DateTime.Now;
        }
    }
}