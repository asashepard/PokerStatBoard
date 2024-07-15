using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.ViewModels
{
    public class JoinPlayerVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Please enter a number up to 2 decimal places")]
        public decimal Amount { get; set; }
    }
}