using GymManagement.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.BLL.ViewModel.HealthRecord;

public class HealthRecordDetailsVM
{
    public int Id { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string BloodType { get; set; } = null!;
    public string? Notes { get; set; } = null!;
}
