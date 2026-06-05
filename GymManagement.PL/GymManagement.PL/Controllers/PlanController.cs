using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;

public class PlanController(IRepository<Plan> plans) : Controller
{
    
    private readonly IRepository<Plan> repository = plans;

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var plans = await repository.GetAllAsync(ct:ct);
          
        return View(plans);
    }
    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var plan = await repository.GetByIdAsync(id, ct);
            

        if (plan is null)
            return RedirectToAction(nameof(Index));
        else     
            return View(plan);
        
    }
}
