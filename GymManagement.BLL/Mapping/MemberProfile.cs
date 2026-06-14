using AutoMapper;
using AutoMapper.Execution;
using GymManagement.BLL.Common;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.DAL.Models;
using GymManagement.DAL.Models.Enums;
using System.Numerics;
using System.Runtime.InteropServices;
using Member = GymManagement.DAL.Models.Member;

namespace GymManagement.BLL.Mapping;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberIndexVM>()
            .ForMember(d => d.PhotoUrl,
            o => o.MapFrom(s => s.Photo))
            .ForMember(d => d.Joindate,
            o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.Gender,
            o => o.MapFrom(s => s.Gender.ToString()));


        CreateMap<Member, MemberDetailsVM>()
            .ForMember(d => d.PhoyoUrl,
            o => o.MapFrom(s => s.Photo))
              .ForMember(d => d.Gender,
            o => o.MapFrom(s => s.Gender.ToString()))
                .ForMember(d => d.DateOfBirth,
            o => o.MapFrom(s => s.BirthDate.ToShortDateString()))
                  .ForMember(d => d.Address,
            o => o.MapFrom(s => $"{s.Address.BuildingNumber} - {s.Address.Street} - {s.Address.City}"))
                  .ForMember(d => d.PlaneName,
                  o => o.MapFrom(s => GetPlanName(s.MembersShips.FirstOrDefault())))
                  .ForMember(d => d.MembershipStartDate,
                  o => o.MapFrom(s => ResolveStartDate(s.MembersShips.FirstOrDefault())))
                    .ForMember(d => d.MembershipEndDate,
                  o => o.MapFrom(s => ResolveEndDate(s.MembersShips.FirstOrDefault())));


        CreateMap<Member, EditMemberVM>()
             .ForMember(d => d.PhotoUrl,
            o => o.MapFrom(s => s.Photo))
               .ForMember(d => d.BuildingNumber,
            o => o.MapFrom(s => s.Address.BuildingNumber))
               .ForMember(d => d.City,
            o => o.MapFrom(s => s.Address.City))
               .ForMember(d => d.Street,
            o => o.MapFrom(s => s.Address.Street));

        CreateMap<EditMemberVM,Member>()
            .ForMember(d=> d.Photo,
            o=>o.MapFrom(s=>s.PhotoUrl))
            .ForMember(d => d.UpdatedAt,
            o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.Address,
            o => o.MapFrom(s => new Address
            {
                BuildingNumber = s.BuildingNumber,
                City = s.City,
                Street = s.Street,
            }));


        CreateMap<CreateMemberVM, Member>()
            .ForMember(d => d.BirthDate,
            o => o.MapFrom(s => s.DateOfBirth))
            .ForMember(d => d.CreatedAt,
            o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.Address,
            o => o.MapFrom(s => new Address
            {
                BuildingNumber = s.BuildingNumber,
                City = s.City,
                Street = s.Street,
            }));
          
    }

    private string ResolveStartDate(Membership? membership)
    => membership?.StartDate.ToShortDateString() ?? "--";

    private string ResolveEndDate(Membership? membership)
=> membership?.EndDate.ToShortDateString() ?? "--";

    private string GetPlanName(Membership? membership)
    => membership?.Plan.Name ?? "No Active Plan";

    
}
