using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Models
{
    public class PlayerModel
    {
        [Key]
        public Guid PlayerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<Guid> BuyInIDs { get; set; }

        [Required]
        public List<Guid> CashOutIDs { get; set; }

        public PlayerModel(string name)
        {
            PlayerID = Guid.NewGuid();
            Name = name;
            BuyInIDs = new List<Guid>();
            CashOutIDs = new List<Guid>();
        }
    }
}