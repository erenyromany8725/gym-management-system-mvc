using GymManagement.DAL.Models;

namespace GymManagement.DAL.Configurations;

internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
{
    public void Configure(EntityTypeBuilder<HealthRecord> builder)
    {
        builder.Property(h => h.Height)
            .HasPrecision(5, 2);
        builder.Property(h => h.Weight)
            .HasPrecision(5, 2);
        builder.Property(h => h.BloodType)
            .HasConversion<string>()
            .HasMaxLength(50);
        builder.HasOne(h=> h.Member)
            .WithOne(m=>m.HealthRecord)
            .HasForeignKey<HealthRecord>(h=>h.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(

                "CK_HealthRecord_Height",
                "[Height] > 0"
                );
        });


        builder.ToTable(t =>
        {
            t.HasCheckConstraint(

                "CK_HealthRecord_Weight",
                "[Weight] > 0"
                );
        });

        builder.HasQueryFilter(h => !h.IsDeleted);
    }
}
