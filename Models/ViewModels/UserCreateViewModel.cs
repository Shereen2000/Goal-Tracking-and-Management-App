using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Goal_Management_Web_App.Models.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}