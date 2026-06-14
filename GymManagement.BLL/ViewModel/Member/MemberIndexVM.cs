using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.ViewModel.Member;

public class MemberIndexVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone {  get; set;} = null!;
    public string? PhotoUrl { get; set; } = null!;
    public DateOnly Birthdate { get; set; }
    public DateTime Joindate { get; set; }
    public string Gender { get; set; } = null!;

}
