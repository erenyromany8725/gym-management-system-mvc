using GymManagement.BLL.Common;
using GymManagement.BLL.Sericess.Interfaces;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.DAL.Models;
using GymManagement.DAL.Models.Enums;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.BLL.Sericess.Classes;

public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    public async Task<Result> CreateAsync(CreateMemberVM model, CancellationToken ct = default)
    {
        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone.Trim();
        var Name = model.Name.Trim();
        

        if (await memberRepository.ExistAsync(m => m.Email == email, ct))
            return Result.Failure("This email is already registered.", nameof(model.Email));
       
        if (await memberRepository.ExistAsync(m => m.Phone == model.Phone, ct))
            return Result.Failure("This Phone Number is already registered.", nameof(model.Phone));

        if (!Enum.TryParse(model.Gender, true, out Gender gender)) 
            return Result.Failure("Invalid Gender.", nameof(model.Gender));
        
        if (!Enum.TryParse(model.healthRecordVM.BloodType, true, out BloodType bloodType))
            return Result.Failure("Invalid Blood Type.", nameof(model.healthRecordVM.BloodType));

        var member = new Member
        {
            Name = model.Name,
            Email = email,
            Phone = model.Phone,
            BirthDate = model.DateOfBirth,
            Gender = gender,
            CreatedAt = DateTime.UtcNow,
            Address = new Address
            { 
                BuildingNumber = model.BuildingNumber,
                City = model.City,
                Street = model.Street,
            },
            HealthRecord = new HealthRecord
            {
                BloodType = bloodType,
                Height = model.healthRecordVM.Height,
                Weight = model.healthRecordVM.Weight,
                Note = model.healthRecordVM.Note,
            }
            

        };
        await memberRepository.AddAsync(member, ct);
        await memberRepository.SaveChangesAsync(ct);
        return Result.Success();

    }

    public async Task<IEnumerable<MemberIndexVM>> GetAllAsync(CancellationToken ct = default)
    {
        var members = await memberRepository.GetAllAsync(ct);
        return members.Select(m => new MemberIndexVM
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            PhotoUrl = m.Photo,
            Phone = m.Phone,
            Joindate = m.CreatedAt,
            Birthdate = m.BirthDate,
            Gender = m.Gender.ToString(),
        });
    }

}
