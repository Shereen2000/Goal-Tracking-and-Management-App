using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Goal_Management_Web_App.Models;

namespace Goal_Management_Web_App.Data.Repositories
{
    public interface IGoalRepository
    {
        Goal GetById(int id);
        IEnumerable<Goal> GetAll(User user);
        void Create(Goal goal);
        void Update(Goal goal);
        void Delete(int id);

    }
}