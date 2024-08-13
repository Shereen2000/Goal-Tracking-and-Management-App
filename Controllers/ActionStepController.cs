using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Data.Repositories;
using Goal_Management_Web_App.Enums;
using Goal_Management_Web_App.Models;
using Goal_Management_Web_App.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Goal_Management_Web_App.Controllers
{

    public class ActionStepController : Controller
    {
        private readonly IActionStepRepository _actionRepo;
        private readonly IGoalRepository _goalRepo;



        public ActionStepController(IActionStepRepository actionRepo, IGoalRepository goalRepo)
        {
            _actionRepo = actionRepo;
            _goalRepo = goalRepo;
        }

        public IActionResult Index(int goalId)
        {
            var goal = _goalRepo.GetById(goalId);

            if (goal == null)
            {
                return NotFound();
            }

            ViewData["GoalId"] = goalId;

            var actionSteps = _actionRepo.GetAll(goal);


            var viewModel = new ActionStepViewModel
            {
                Actions = actionSteps
            };

            return View(viewModel);
        }

        // GET: ActionStep/Edit/5
        // GET: ActionStep/Edit/5
        public IActionResult Edit(int id)
        {
            var actionStep = _actionRepo.GetById(id);
            if (actionStep == null)
            {
                return NotFound();
            }
            return View(actionStep);
        }

        // POST: ActionStep/Edit
        [HttpPost]
        public IActionResult Edit(ActionStep actionStep)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _actionRepo.Update(actionStep);
                    TempData["Message"] = "Action step updated successfully.";
                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View(actionStep);
        }


        [HttpPost]
        public IActionResult Delete(int id, int goalId)
        {
            try
            {
                _actionRepo.Delete(id);
                TempData["Message"] = "Action step deleted successfully.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", new { goalId });
        }

        public IActionResult Details(int id)
        {
            var actionStep = _actionRepo.GetById(id);
            if (actionStep == null)
            {
                return NotFound();
            }
            return View(actionStep);
        }

        public IActionResult Add(int goalId)
        {
            // Prepare a new ActionStep instance and set the GoalId
            var actionStep = new ActionStep
            {
                GoalId = goalId,
                DueDate = DateTime.Today // Set default DueDate to today's date
            };

            return View(actionStep);
        }

        [HttpPost]
        public IActionResult Add(ActionStep actionStep)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _actionRepo.Create(actionStep);
                    TempData["Message"] = "Action step added successfully.";
                    return RedirectToAction("Index", new { goalId = actionStep.GoalId });
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            // If there are validation errors or the operation failed, return to the form
            return View(actionStep);
        }


        public IActionResult ActionStepTrackingBoard(int goalId)
        {
            var goal = _goalRepo.GetById(goalId);
            if (goal == null)
            {
                return NotFound();
            }

            var actionSteps = _actionRepo.GetAll(goal).ToList();

            var viewModel = new ActionStepTrackingBoardViewModel
            {
                Goal = goal,
                Actions = actionSteps
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult UpdateStatus([FromBody] ActionStepStatusUpdateViewModel model)
        {
            try
            {
                var action = _actionRepo.GetById(model.ActionStepId);
                if (action == null)
                {
                    return Json(new { success = false, message = "action not found" });
                }

                // Ensure the status value is valid
                if (!Enum.IsDefined(typeof(Status), model.NewStatus))
                {
                    return Json(new { success = false, message = "Invalid status" });
                }

                action.Status = model.NewStatus;
                _actionRepo.Update(action);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}