using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Models
{
    public class PokerGameModel
    {
        [Key]
        public Guid PokerGameID { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public PokerGameModel()
        {
            PokerGameID = Guid.NewGuid();
            StartDateTime = DateTime.Now;
        }
    }
}