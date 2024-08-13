using Goal_Management_Web_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Goal_Management_Web_App.Data.Repositories
{
    public class ActionStepRepository : IActionStepRepository
    {
        private readonly AppDbContext _context;

        public ActionStepRepository(AppDbContext context)
        {
            
            _context = context;
        }
        public void Create(ActionStep step)
        {
            _context.Actions.Add(step);
            _context.SaveChanges();
        }
    

        public void Delete(int id)
        {    
            var actionStep = _context.Actions.Find(id);
            if (actionStep == null)
            {
                throw new InvalidOperationException("Action Step not found.");
            }

            _context.Actions.Remove(actionStep);
            _context.SaveChanges();
       
    }

        public IEnumerable<ActionStep> GetAll(Goal goal)
        {
            return _context.Actions
                   .Where(ActionStep => ActionStep.GoalId == goal.GoalId)
                   .ToList();
        }

        public ActionStep GetById(int id)
        {
            return _context.Actions.Find(id);
        }

        public void Update(ActionStep step)
        {
            var existingActionStep = _context.Actions.Find(step.ActionStepId);

            if (existingActionStep == null)
            {
                // Handle the case where the goal was not found
                throw new InvalidOperationException("Action Step not found.");

            }

            existingActionStep.Subject= step.Subject;
            existingActionStep.Status =  step.Status;
            existingActionStep.Description = step.Description;
            existingActionStep.DueDate = step.DueDate;

            _context.SaveChanges();
        }
    }
}
