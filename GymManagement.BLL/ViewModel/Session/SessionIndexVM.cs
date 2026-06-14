using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GymManagement.BLL.ViewModel.Session;

public class SessionIndexVM
{
    public int Id { get; init; }
    public string Speciality { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TraineerName { get; set; } = null!;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public int BookedCount { get; init; }
    public int Capacity { get; init; }
    public SessionStatus Status { get; set; }
    public TimeSpan Duration => EndDate - StartDate;
    public string HeaderClass => Status switch
    {
        SessionStatus.Upcoming => "bg-primary",
        SessionStatus.Ongoing => "bg-success",
        SessionStatus.Completed => "bg-secondary",
        _=> "bg-secondary"
    };


}
