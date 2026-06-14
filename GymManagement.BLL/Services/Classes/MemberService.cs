using AutoMapper;
using GymManagement.BLL.Common;
using GymManagement.BLL.Mapping;
using GymManagement.BLL.Servicess.Interfaces;
using GymManagement.BLL.ViewModel.HealthRecord;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.DAL.Models;
using GymManagement.DAL.Models.Enums;
using GymManagement.DAL.Repositories.Interfaces;

public class MemberService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDateTimeProvider clock) : IMemberService
{

    public async Task<Result<IEnumerable<MemberIndexVM>>> GetAllAsync(CancellationToken ct = default)
    {
        var members = await unitOfWork.Members.GetAllAsync(ct);

        if (!members.Any())
            return Result<IEnumerable<MemberIndexVM>>.Failure("No members found.");


        return Result<IEnumerable<MemberIndexVM>>.Success(mapper.Map<IEnumerable<MemberIndexVM>>(members));
    }

    public async Task<Result> CreateAsync(CreateMemberVM model, CancellationToken ct = default)
    {
        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone.Trim();
        var name = model.Name.Trim();

        if (await unitOfWork.Members.IsEmailTakenAsync(email, null, ct))
            return Result.Failure("This email is already registered.", nameof(model.Email));

        if (await unitOfWork.Members.IsPhoneTakenAsync(phone, null, ct))
            return Result.Failure("This phone number is already registered.", nameof(model.Phone));

        if (!Enum.TryParse(model.Gender, true, out Gender gender))
            return Result.Failure("Invalid gender.", nameof(model.Gender));


        await unitOfWork.Members.AddAsync(mapper.Map<Member>(model), ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }

    public async Task<Result<MemberDetailsVM>> GetDetailsAsync(int id, CancellationToken ct = default)
    {
        var member = await unitOfWork.Members.GetWithMembershipAsync(id, clock.UTCNow, ct);

        if (member is null)
            return Result<MemberDetailsVM>.Failure("Member not found.", nameof(id));

        return Result<MemberDetailsVM>.Success(mapper.Map<MemberDetailsVM>(member));
       
    }

    public async Task<Result<HealthRecordDetailsVM>> GetHealthRecordAsync(int id, CancellationToken ct = default)
    {
        var member = await unitOfWork.Members.GetByIdAsync(id, includes: [m => m.HealthRecord], ct: ct);

        if (member is null)
            return Result<HealthRecordDetailsVM>.Failure("Member not found.", nameof(id));

        if (member.HealthRecord is null)
            return Result<HealthRecordDetailsVM>.Failure("Health record not found.", nameof(id));

        return Result<HealthRecordDetailsVM>.Success(mapper.Map<HealthRecordDetailsVM>(member.HealthRecord));
    }

    public async Task<Result<EditMemberVM>> GetForUpdateAsync(int id, CancellationToken ct = default)
    {
        var member = await unitOfWork.Members.GetByIdAsync(id, ct: ct);

        if (member is null)
            return Result<EditMemberVM>.Failure("Member not found.", nameof(id));

        return Result<EditMemberVM>.Success(mapper.Map<EditMemberVM>(member));
    }

    public async Task<Result> UpdateAsync(int id, EditMemberVM model, CancellationToken ct = default)
    {
        var member = await unitOfWork.Members.GetByIdAsync(id, ct: ct);

        if (member is null)
            return Result.Failure("Member not found.", nameof(id));

        if (!string.Equals(member.Name, model.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            return Result.Failure("Name cannot be changed.", nameof(model.Name));

        var normalizedEmail = model.Email.Trim().ToLowerInvariant();
        var normalizedPhone = model.Phone.Trim();

        if (await unitOfWork.Members.IsEmailTakenAsync(normalizedEmail, id, ct))
            return Result.Failure("This email is already registered.", nameof(model.Email));

        if (await unitOfWork.Members.IsPhoneTakenAsync(normalizedPhone, id, ct))
            return Result.Failure("This phone number is already registered.", nameof(model.Phone));

        mapper.Map(model, member);
        await unitOfWork.Members.UpdateAsync(member, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var member = await unitOfWork.Members.GetByIdAsync(
            id,
            includes: [m => m.HealthRecord],
            ct: ct);

        if (member is null)
            return Result.Failure("Member not found.", nameof(id));

        if (await unitOfWork.Bookings.HasUpcomingBookingsAsync(id, clock.UTCNow, ct))
            return Result.Failure("Cannot delete a member with upcoming bookings.", nameof(id));

        await unitOfWork.Members.SoftDeleteAsync(member, ct);
        await unitOfWork.HealthRecords.SoftDeleteAsync(member.HealthRecord, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }



}