using GymManagement.BLL.ViewModel.HealthRecord;
using GymManagement.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModel.Trainer;

public class CreateTrainerVM
{
    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
    public string Name { get; set; } = null!;


    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be a valid Egyptian mobile number")]

    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; } = null!;

    [Required(ErrorMessage = "Building number is required")]
    [Range(1, 9000, ErrorMessage = "Building number must be greater than 0")]
    public int BuildingNumber { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 150 characters")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street can only contain letters, numbers and spaces")]
    public string Street { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "Speciality is required")]

    public Speciality Speciality { get; set; }

}


 



