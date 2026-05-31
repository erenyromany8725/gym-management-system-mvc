using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(m => m.CreatedAt)
             .HasColumnName("BookingDate");

        builder.Property(b => b.IsAttended)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(b => new
        {
            b.MemberId,
            b.SessionId,
        }).IsUnique();


        builder.ToTable(t =>
        {
            t.HasCheckConstraint(


                "CK_Booking_Date", 
                "[BookingDate] >= GETDATE()");
        });
    }
}
