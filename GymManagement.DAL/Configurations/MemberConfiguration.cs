using GymManagement.DAL.Models;

namespace GymManagement.DAL.Configurations;

public class MemberConfiguration : UserConfiguration<Member>, IEntityTypeConfiguration<Member>
{
    public override void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(m => m.CreatedAt)
            .HasColumnName("JoinDate");

        builder.Property(m => m.Photo)
            .HasMaxLength(500);


        base.Configure(builder);
    }
}
