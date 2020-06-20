using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.SQLite.Generic;

namespace VirusAlertAPI.DB
{
    [Table("Users")]
    public class AppUser : IUser
    {
        [Key]
        public int usrId { get; set; }
        public string usrName { get; set; }
        public string usrEmail { get; set; }
        public string usrPassword { get; set; }
        public string usrOms { get; set; }
        public int usrRegId { get; set; }
        public string usrPasport { get; set; }

        public AppUser()
        {
            usrId = -1;
        }

        [NotMapped]
        public virtual string Id { get; set; }
        [NotMapped]
        public string UserName
        {
            get
            {
                return usrName;
            }
            set
            {
                usrName = value;
            }
        }
        [NotMapped]
        public string Email
        {
            get
            {
                return usrEmail;
            }
            set
            {
                usrEmail = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<DB.AppUser> manager, string authenticationType)
        {
            var userIdentity = new ClaimsIdentity(authenticationType);
            return userIdentity;
        }
    }
}