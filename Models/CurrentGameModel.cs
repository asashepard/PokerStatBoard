using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web;

namespace PokerStatBoard.Models
{
    public class CurrentGameModel
    {
        [Key]
        public Guid CurrentGameID { get; set; }
        
        public Guid PokerGameID { get; set; }

        public CurrentGameModel()
        {
            CurrentGameID = Guid.NewGuid();
            PokerGameID = Guid.Empty;
        }
    }
}