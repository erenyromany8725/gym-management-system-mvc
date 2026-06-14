using GymManagement.BLL.Servicess.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class MemberController(IMemberService memberService) : Controller
{ 
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await memberService.GetAllAsync(ct);
        return View(result.IsSuccess ? result.Value : []);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMemberVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await memberService.CreateAsync(model, ct);
        if (result.IsFailure)
        {
            ModelState.AddModelError(result.ErrorKey ?? string.Empty, result.Error!);
            TempData["Error"] = "Cannot create a member.";
            return View(model);
        }

        TempData["Success"] = "Member created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var result = await memberService.GetDetailsAsync(id, ct);
        if (result.IsFailure)
            return NotFound();

        return View(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
    {
        var result = await memberService.GetHealthRecordAsync(id, ct);
        if (result.IsFailure)
            return NotFound();

        return View(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var result = await memberService.GetForUpdateAsync(id, ct);
        if (result.IsFailure)
            return NotFound();

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditMemberVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await memberService.UpdateAsync(id, model, ct);
        if (result.IsFailure)
        {
            ModelState.AddModelError(result.ErrorKey ?? string.Empty, result.Error!);
            TempData["Error"] = "Cannot update member.";
            return View(model);
        }

        TempData["Success"] = "Member updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await memberService.GetDetailsAsync(id, ct);
        if (result.IsFailure)
            return NotFound();
        ViewBag.id = id;
        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var result = await memberService.DeleteAsync(id, ct);
        if (result.IsFailure)
        {
            TempData["Error"] = result.Error;
            return RedirectToAction(nameof(Delete), new { id });
        }

        TempData["Success"] = "Member deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}