using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    Task<bool> HasUpcomingBookingsAsync(int memberId, DateTime utcNow, CancellationToken ct = default);

}
