using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Enums;

namespace Goal_Management_Web_App.Models
{
    public class ActionStep
    {
        public int ActionStepId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }

        public int GoalId { get; set; }
        public Goal Goal { get; set; }

    }
}  