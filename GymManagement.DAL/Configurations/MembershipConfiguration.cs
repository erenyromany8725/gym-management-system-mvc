using GymManagement.DAL.Models;

namespace GymManagement.DAL.Configurations;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {

        builder.ToTable(t =>
        { 

            t.HasCheckConstraint(
                "CK_Membership_DateRange",
                "[EndDate] > [StartDate]"
                );
        });

        //builder.HasIndex(ms => new
        //{
        //    ms.MemberId,
        //    ms.PLanId,
        //    ms.StartDate
        //}).IsUnique();

    }
}
