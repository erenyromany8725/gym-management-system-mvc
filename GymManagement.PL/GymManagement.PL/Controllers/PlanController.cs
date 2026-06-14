using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.Servicess.Interfaces;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;

public class PlanController(IPlanService planService) : Controller
{


    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var plans = await planService.GetAllAsync();

        return View(plans.Value);
    }

    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var plan = await planService.GetDetailsAsync(id);

        if (plan is null)
            return RedirectToAction(nameof(Index));

        return View(plan.Value);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var plan = await planService.GetForUpdateAsync(id, ct);
        if (plan.IsFailure)
            return NotFound();

        return View(plan.Value);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditPlanVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);
        var plan = await planService.UpdateAsync(id, model, ct);
        if (plan.IsFailure)
        {
            ModelState.AddModelError(plan.ErrorKey ?? string.Empty, plan.Error!);
            TempData["Error"] = "Cannot update plan.";
            return View(model);
        }
        TempData["Success"] = "Plan updated successfully.";
        return RedirectToAction(nameof(Index));

    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus(int id, CancellationToken ct)
    {
        var plan = await planService.ChangeStatusAsync(id, ct);
        if (plan.IsFailure) 
        { 
            TempData["Error"] = "Cannot deactivate plan with active membership.";
            return RedirectToAction(nameof(Index));

        }
        TempData["Success"] = "Plan updated successfully.";
        return RedirectToAction(nameof(Index));


    }


}

