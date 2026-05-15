using GymManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnType("Varchar")
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasColumnType("Varchar")
            .HasMaxLength(200);

        builder.Property(p => p.Price)
            .HasPrecision(10, 2);

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        
    }

    
}

