using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Models;

namespace Goal_Management_Web_App.Data.Repositories
{

    public class GoalRepository : IGoalRepository
    {
        private readonly AppDbContext _context;

        public GoalRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Goal goal)
        {
            _context.Goals.Add(goal);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var goal = _context.Goals.Find(id);
            if (goal == null)
            {
                throw new InvalidOperationException("Goal not found.");
            }

            _context.Goals.Remove(goal);
            _context.SaveChanges();
        }

        public IEnumerable<Goal> GetAll(User user)
        {
                return _context.Goals
                   .Where(goal => goal.UserId == user.Id)
                   .ToList();
        }

        public Goal GetById(int id)
        {
            return  _context.Goals.Find(id);
        }

        public void Update(Goal goal)
        {
            var existingGoal = _context.Goals.Find(goal.GoalId);

            if (existingGoal == null)
            {
                // Handle the case where the goal was not found
                throw new InvalidOperationException("Goal not found.");

            }

            existingGoal.Title = goal.Title;
            existingGoal.Status = goal.Status;
            existingGoal.Description = goal.Description;
            existingGoal.Motivation = goal.Motivation;

            _context.SaveChanges();

        }
    }
}