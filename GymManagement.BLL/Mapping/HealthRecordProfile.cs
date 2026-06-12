using AutoMapper;
using GymManagement.BLL.ViewModel.HealthRecord;
using GymManagement.DAL.Models;
using GymManagement.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace GymManagement.BLL.Mapping;

public class HealthRecordProfile : Profile
{
    public HealthRecordProfile()
    {
        CreateMap<HealthRecordCreateVM, HealthRecord>();
       
        CreateMap<HealthRecord,HealthRecordDetailsVM>()
                .ForMember(d => d.BloodType,
                o => o.MapFrom(s => BloodTypeDisplay(s.BloodType)));
    }

    

    private static string BloodTypeDisplay(BloodType bloodType) => bloodType switch
    {
        BloodType.A_Positive => "A+",
        BloodType.A_Negative => "A-",
        BloodType.B_Positive => "B+",  
        BloodType.B_Negative => "B-",
        BloodType.O_Positive => "O+",
        BloodType.O_Negative => "O-",
        BloodType.AB_Positive => "AB+",
        BloodType.AB_Negative => "AB-",
        _ => bloodType.ToString()
    };

 

}
