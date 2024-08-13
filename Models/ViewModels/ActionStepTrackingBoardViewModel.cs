namespace Goal_Management_Web_App.Models.ViewModels
{
    public class ActionStepTrackingBoardViewModel
    {
        public Goal Goal { get; set; }
        public IEnumerable<ActionStep> Actions { get; set; }
    }
}
