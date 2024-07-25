using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Models
{
    public class GroupModel
    {
        [Key]
        public Guid GroupID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid PokerGameID { get; set; }

        public GroupModel()
        {
            GroupID = Guid.NewGuid();
            Name = "";
            PokerGameID = Guid.Empty;
        }

        public GroupModel(string Name)
        {
            GroupID = Guid.NewGuid();
            this.Name = Name;
            PokerGameID = Guid.Empty;
        }
    }
}