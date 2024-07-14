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

        public PlayerModel()
        {
            PlayerID = Guid.NewGuid();
        }

        public PlayerModel(string name)
        {
            PlayerID = Guid.NewGuid();
            Name = name;
        }
    }
}