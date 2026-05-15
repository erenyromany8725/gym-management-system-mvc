using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Models;

public class Session : BaseEntity
{
    public string Description { get; set; } = null!;
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

