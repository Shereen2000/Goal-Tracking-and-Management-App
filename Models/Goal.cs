using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client.AppConfig;

namespace Goal_Management_Web_App.Models
{
    public class Goal
    {
        public int GoalId { get; set; }

        [Required]
        public string Title { get; set; }
        public Status Status { get; set; }
        public string Motivation { get; set; }
        public string Description { get; set; }
        public ICollection<ActionStep> ActionsSteps {get; set;}
        public string UserId { get; set; }
        public User User { get; set; }
    }
}