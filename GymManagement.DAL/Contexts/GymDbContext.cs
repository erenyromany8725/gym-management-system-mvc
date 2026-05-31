using GymManagement.DAL.Models;
using GymManagementSystem.Interceptors;

namespace GymManagementSystem.Contexts;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
        
    }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Traineer> Traineers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Membership> Memberships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
