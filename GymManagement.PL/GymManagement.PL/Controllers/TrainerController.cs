using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.Servicess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers;

public class TrainerController(ITrainerService trainerService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var trainers = await trainerService.GetAllAsync(ct);
        return View(trainers.IsSuccess ? trainers.Value : []);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTrainerVM model , CancellationToken ct)
    {
        if(!ModelState.IsValid)
            return View(model);


        var trainer = await trainerService.CreateAsync(model, ct);
        if (trainer.IsFailure)
        {
            ModelState.AddModelError(trainer.ErrorKey ?? string.Empty, trainer.Error!);
            TempData["Error"] = "Cannot create a Trainer.";
            return View(model);
        }

        TempData["Success"] = "Trainer created successfully.";
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var trainer = await trainerService.GetDetailsAsync(id,ct);
        return View(trainer.Value);
    }
   
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var trainer = await trainerService.GetForUpdateAsync(id, ct);
        if (trainer.IsFailure)
            return NotFound();

        return View(trainer.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, UpdateTrainerVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        var trainer = await trainerService.UpdateAsync(id, model, ct);
        if (trainer.IsFailure)
        {
            ModelState.AddModelError(trainer.ErrorKey ?? string.Empty, trainer.Error!);
            TempData["Error"] = "Cannot update trainer.";
            return View(model);
        }

        TempData["Success"] = "Trainer updated successfully.";
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var trainer = await trainerService.GetForUpdateAsync(id, ct);

        if (trainer.IsFailure)
            return NotFound();
        ViewBag.id = id;
        return View(trainer.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var trainer = await trainerService.DeleteAsync(id, ct);
        if (trainer.IsFailure)
        {
            TempData["Error"] = trainer.Error;
            return RedirectToAction(nameof(Delete), new { id });
        }

        TempData["Success"] = "Trainer deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

}


    