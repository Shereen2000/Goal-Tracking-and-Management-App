using Goal_Management_Web_App.Models;

namespace Goal_Management_Web_App.Data.Repositories
{
    public interface IActionStepRepository
    {
        ActionStep GetById(int id);
        IEnumerable<ActionStep> GetAll(Goal goal);
        void Create(ActionStep step);
        void Update(ActionStep step);
        void Delete(int id);

    }
}
