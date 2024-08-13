using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Goal_Management_Web_App.Models.ViewModels
{
    public class GoalCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Motivation { get; set; }
    }
}