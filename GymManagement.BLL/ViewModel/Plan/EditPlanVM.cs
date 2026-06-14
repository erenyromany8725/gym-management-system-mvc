using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModel.Plan;

public class EditPlanVM
{
    public string Name { get; set; } = null!;

    [Range(1, 365, ErrorMessage = "Plan duration days must be between 1 and 365 days.")]

    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
}
