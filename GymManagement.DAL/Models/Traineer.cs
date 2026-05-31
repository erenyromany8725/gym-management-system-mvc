using GymManagement.DAL.Models.Enums;

namespace GymManagement.DAL.Models;

public class Traineer : User
{
    public Speciality Speciality { get; set; }

    public ICollection<Session> Sessions { get; set; } = [];
}
