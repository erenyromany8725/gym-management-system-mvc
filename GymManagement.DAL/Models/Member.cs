namespace GymManagement.DAL.Models;

public class Member : User
{
    public string? Photo { get; set; }

    public HealthRecord HealthRecord { get; set; } = null!;

    public ICollection<Membership> MembersShips { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
    


}
