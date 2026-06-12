using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.ViewModel.Member;

public class MemberDetailsVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? PhoyoUrl { get; set; }
    public string Email { get; set; } = null!;
    public string Phone {  get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PlaneName { get; set; } = null!;
    public string MembershipStartDate { get; set; } = null!;
    public string MembershipEndDate { get; set; } = null!;

}
