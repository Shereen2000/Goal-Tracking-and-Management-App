using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goal_Management_Web_App.Models.ViewModels
{
    public class GoalListViewModel
    {
        public IEnumerable<Goal> Goals{ get; set; }
    }
}