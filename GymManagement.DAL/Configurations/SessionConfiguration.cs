using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint("SessionCapacityCheck", "Capacity BETWEEN 1 AND 25");
            tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
        });

        builder.HasQueryFilter(s => !s.IsDeleted);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Session_Capacity",
                "[Capacity] BETWEEN 1 AND 25"
                
                );

            t.HasCheckConstraint(
                "CK_Session_DateRange",
                "[EndDate] > [StartDate]"
                );
        });

        builder.HasQueryFilter(s => !s.IsDeleted);
    }


}
