using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.DAL.Repositories.Classes;

public class BookingRepository(GymDbContext context) : Repository<Booking>(context), IBookingRepository
{ private readonly GymDbContext _dbContext = context;
    public async Task<bool> HasUpcomingBookingsAsync(int memberId, DateTime utcNow, CancellationToken ct = default)
    => await _dbContext.Set<Booking>()
        .AnyAsync(b => b.MemberId == memberId && b.Session.EndDate >= utcNow, ct);
        
        
}
