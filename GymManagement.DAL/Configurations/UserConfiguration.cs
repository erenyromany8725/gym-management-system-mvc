using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GymManagement.DAL.Configurations;

public class UserConfiguration<T> : IEntityTypeConfiguration<T> where T : User
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(u => u.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(u => u.Phone)
            .HasMaxLength(11);

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasIndex(u => u.Phone)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint(
                "EmailCheck",
                "Email LIKE '%_@_%.__%'");

            tb.HasCheckConstraint(
                "PhoneCheck",
                "LEN(Phone) = 11 AND Phone LIKE '01[0125][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

            address.Property(a => a.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

            address.Property(a => a.BuildingNumber)
                .HasColumnName("BuildingNumber");
        });

        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}
