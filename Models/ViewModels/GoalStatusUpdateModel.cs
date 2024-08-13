using Goal_Management_Web_App.Enums;

public class GoalStatusUpdateModel
{
    public int GoalId { get; set; }
    public Status NewStatus { get; set; }
}