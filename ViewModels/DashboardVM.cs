using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerStatBoard.ViewModels
{
    public class DashboardVM
    {
        public List<Models.GroupModel> groupModels { get; set; }

        public Guid userID { get; set; }
    }
}