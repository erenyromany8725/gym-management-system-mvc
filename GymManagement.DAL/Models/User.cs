using GymManagement.DAL.Models.Enums;

namespace GymManagement.DAL.Models;

public abstract class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public Address Address { get; set; } = null!;
    public Gender Gender { get; set; }

}




[Owned]
public class Address()
{
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public int BuildingNumber { get; set; }
}
