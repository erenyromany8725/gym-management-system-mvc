using GymManagement.DAL.Models;
using GymManagement.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModel.Trainer;

public class TrainerDetailsVM
{
   public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Speciality { get; set; } = null!;

}


 





