using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<Session> Sessions { get; set; } = []; 
}

