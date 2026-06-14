using GymManagement.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.ViewModel.Trainer;

public class TrainerIndexVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public Speciality Speciality { get; set; }
}
