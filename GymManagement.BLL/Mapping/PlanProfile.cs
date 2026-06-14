using AutoMapper;
using GymManagement.BLL.ViewModel.Plan;
using GymManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Mapping;

public class PlanProfile : Profile
{
    public PlanProfile()
    {
        CreateMap<Plan, PlanDetailsVM>();
        CreateMap<Plan, EditPlanVM>();
        CreateMap<EditPlanVM, Plan>();
    }
}
