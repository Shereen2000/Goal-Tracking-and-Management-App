using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Goal_Management_Web_App.Models
{
    public class User:IdentityUser
    {
        public ICollection<Goal> Goals{ get; set; }
        public string Name { get; set; }
    }
}