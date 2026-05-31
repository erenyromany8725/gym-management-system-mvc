using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;

public class PlanController : Controller
{
    //private readonly GymDbContext _context;
    private readonly IPlanRepository planRepository;
    public PlanController(IPlanRepository repository)
    {
        planRepository = repository;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var plans = await planRepository.GetAllAsync(ct:ct);
          
        return View(plans);
    }
    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var plan = await planRepository.GetByIdAsync(id, ct);
            

        if (plan is null)
            return RedirectToAction(nameof(Index));
        else     
            return View(plan);
        
    }
}
