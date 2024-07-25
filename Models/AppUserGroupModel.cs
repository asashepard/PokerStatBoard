using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokerStatBoard.Models
{
    public class AppUserGroupModel
    {
        [Key]
        public Guid ApplicationUserGroupID { get; set; }

        [Required]
        public Guid ApplicationUserID { get; set; }

        [Required]
        public Guid GroupID { get; set; }

        public AppUserGroupModel()
        {
            ApplicationUserGroupID = Guid.NewGuid();
        }

        public AppUserGroupModel(Guid ApplicationUserID, Guid GroupID)
        {
            ApplicationUserGroupID = Guid.NewGuid();
            this.ApplicationUserID = ApplicationUserID;
            this.GroupID = GroupID;
        }
    }
}