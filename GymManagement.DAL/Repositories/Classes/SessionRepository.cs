using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes;

public class SessionRepository(GymDbContext context) : Repository<Session>(context), ISessionRepository

{
}
