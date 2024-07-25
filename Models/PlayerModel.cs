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
        public bool IsPlaying { get; set; }

        [Required]
        public Guid GroupID { get; set; }

        public PlayerModel()
        {
            PlayerID = Guid.NewGuid();
            IsPlaying = false;
        }

        public PlayerModel(string name, Guid groupID)
        {
            PlayerID = Guid.NewGuid();
            Name = name;
            IsPlaying = false;
            GroupID = groupID;
        }
    }
}