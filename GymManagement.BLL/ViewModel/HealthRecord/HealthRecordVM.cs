using GymManagement.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModel.HealthRecord;

public class HealthRecordVM
{
    [Range(0.1, 300,ErrorMessage ="Heigth must be greater than 0")]
    public decimal Height{ get; set; }
    [Range(0.1, 500, ErrorMessage = "Weight must be greater than 0")]
    public decimal Weight { get; set; }
    [Required(ErrorMessage = "Blood type is required")]

    public string BloodType { get; set; } = null!;
    public string? Note { get; set; } = null!;
}
