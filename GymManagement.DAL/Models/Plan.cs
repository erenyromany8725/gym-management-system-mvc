using GymManagement.DAL.Models;

namespace GymManagementSystem.Models;

public class Plan :BaseEntity
{

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DurationDays { get; set; }
    public decimal Price { get; set; }

    public bool IsActive { get; set; }
    public IList<Membership> Memberships { get; set; } = [];
}
