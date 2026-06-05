using GymManagement.BLL.Sericess.Interfaces;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers;

public class MemberController(IMemberService member) : Controller
{
    public async  Task<IActionResult>Index(CancellationToken ct)
    {
        var items = await member.GetAllAsync(ct);
        return View(items);
    }
    [HttpGet]
    public async Task<IActionResult>Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberVM model ,CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await member.CreateAsync(model, ct);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(result.ErrorKey,result.Error!);

            return View(model);
        }

        return RedirectToAction(nameof(Index));
        
    }
}
