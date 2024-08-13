using Goal_Management_Web_App.Data.Repositories;
using Goal_Management_Web_App.Models.ViewModels;
using Goal_Management_Web_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Goal_Management_Web_App.Enums;

public class GoalController : Controller
{
    private readonly IGoalRepository _repo;
    private readonly UserManager<User> _userManager;

    public GoalController(IGoalRepository repo, UserManager<User> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    [TempData]
    public string Message { get; set; }

    public async Task<IActionResult> Index()
    {
        var username = User.Identity.Name;

        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var goals = _repo.GetAll(user).ToList();

        return View(new GoalListViewModel
        {
            Goals = goals
        });
    }

    // GET: /Goal/Add
    public IActionResult Add()
    {
        var username = User.Identity.Name;

        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new Goal());
    }

    // POST: /Goal/Add
    [HttpPost]
    public async Task<IActionResult> Add(Goal goal)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var username = User.Identity.Name;

                var user = await _userManager.FindByNameAsync(username);

                goal.Status = 0; // Set default status to 0
                goal.UserId = user.Id; // Associate with the user

                _repo.Create(goal);

                Message = "Goal added successfully.";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }
        return View(goal);
    }

    // GET: /Goal/Edit/5
    public IActionResult Edit(int id)
    {
        var goal = _repo.GetById(id);
        if (goal == null)
        {
            return NotFound();
        }
        return View(goal);
    }

    // POST: /Goal/Edit
    [HttpPost]
    public async Task<IActionResult> Edit(Goal goal)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);

                var existingGoal = _repo.GetById(goal.GoalId);
                if (existingGoal == null || existingGoal.UserId != user.Id)
                {
                    return NotFound();
                }

                existingGoal.Title = goal.Title;
                // Status should not be updated, keep the existing one
                existingGoal.Description = goal.Description;
                existingGoal.Motivation = goal.Motivation;

                _repo.Update(existingGoal);

                Message = "Goal updated successfully.";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }
        return View(goal);
    }


    // POST: /Goal/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        try
        {
            var goal = _repo.GetById(id);
            if (goal == null)
            {
                return NotFound();
            }

            // Ensure that the goal belongs to the current user
            var username = User.Identity.Name;
            var user = _userManager.FindByNameAsync(username).Result;
            if (goal.UserId != user.Id)
            {
                return Unauthorized();
            }

            _repo.Delete(id);

            Message = "Goal deleted successfully.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View("Error"); // Ensure you have an Error view to show the error message
        }
    }

    [HttpPost]
    public IActionResult UpdateStatus([FromBody] GoalStatusUpdateModel model)
    {
        try
        {
            var goal = _repo.GetById(model.GoalId);
            if (goal == null)
            {
                return Json(new { success = false, message = "Goal not found" });
            }

            // Ensure the status value is valid
            if (!Enum.IsDefined(typeof(Status), model.NewStatus))
            {
                return Json(new { success = false, message = "Invalid status" });
            }

            goal.Status = model.NewStatus;
            _repo.Update(goal);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    public async Task<IActionResult> GoalTrackingBoard()
    {
        var username = User.Identity.Name;

        if (username == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Fetch the goals for the user
        var goals = _repo.GetAll(user).ToList();

        // Pass goals to the view model
        var viewModel = new GoalListViewModel
        {
            Goals = goals
        };

        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var goal = _repo.GetById(id);
        if (goal == null)
        {
            return NotFound();
        }

        return View(goal);
    }

}
