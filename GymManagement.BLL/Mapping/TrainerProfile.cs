using AutoMapper;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.BLL.ViewModel.Trainer;
using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Mapping;

public class TrainerProfile :Profile
{
    public TrainerProfile()
    {
        CreateMap<Traineer, TrainerIndexVM>();

        CreateMap<CreateTrainerVM, Traineer>()
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

        CreateMap<Traineer, TrainerDetailsVM>()
          .ForMember(d => d.DateOfBirth,
          o => o.MapFrom(s => s.BirthDate.ToShortDateString()))
           .ForMember(d => d.Address,
            o => o.MapFrom(s => $"{s.Address.BuildingNumber} - {s.Address.Street} - {s.Address.City}"));

        CreateMap<Traineer, UpdateTrainerVM>()
           .ForMember(d => d.BuildingNumber,
            o => o.MapFrom(s => s.Address.BuildingNumber))
            .ForMember(d => d.City,
            o => o.MapFrom(s => s.Address.City))
             .ForMember(d => d.Street,
            o => o.MapFrom(s => s.Address.Street));

        CreateMap<UpdateTrainerVM, Traineer>()
            .ForMember(d => d.UpdatedAt,
            o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.Address,
            o => o.MapFrom(s => new Address
            {
                BuildingNumber = s.BuildingNumber,
                City = s.City,
                Street = s.Street,
            }));



    }
}
