using GymManagement.DAL.Models.Enums;

namespace GymManagement.DAL.Models;

public class HealthRecord : BaseEntity
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string? Note { get; set; }
    public BloodType BloodType { get; set; }
}