using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Models;

public class Membership : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int PLanId { get; set; }
    public Plan Plan { get; set; } = null!;
}
