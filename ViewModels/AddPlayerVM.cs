using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace PokerStatBoard.ViewModels
{
    public class AddPlayerVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2)]
        [MaxLength(11)]
        public string Name { get; set; }
    }
}