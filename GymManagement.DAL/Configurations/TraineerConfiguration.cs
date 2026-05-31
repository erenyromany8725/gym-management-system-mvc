using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Configurations;

public class TraineerConfiguration : UserConfiguration<Traineer> , IEntityTypeConfiguration<Traineer>
{
    public override void Configure(EntityTypeBuilder<Traineer> builder)
    {
        builder.Property(t => t.CreatedAt)
            .HasColumnName("HireDate");

        builder.Property(t => t.Speciality)
            .HasConversion<string>()
            .HasMaxLength(30);

        base.Configure(builder);
    }
}
