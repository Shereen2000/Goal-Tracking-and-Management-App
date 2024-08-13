using Goal_Management_Web_App.Enums;

namespace Goal_Management_Web_App.Models.ViewModels
{
    public class ActionStepStatusUpdateViewModel
    {
        public int ActionStepId { get; set; }
        public Status NewStatus { get; set; }
    }
}
