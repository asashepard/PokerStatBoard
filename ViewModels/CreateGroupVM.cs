using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.ViewModels
{
    public class CreateGroupVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2)]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}