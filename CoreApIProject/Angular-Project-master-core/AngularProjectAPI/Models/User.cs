using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class User:IdentityUser
    {
        public User(string userName) : base(userName)
        {
        }
        public User() : base()
        {
        }
        public string Gender { get; set; }

        public string Image { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
