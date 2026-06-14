using AutoMapper;
using GymManagement.BLL.Common;
using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModel.Trainer;
using GymManagement.DAL.Models;
using GymManagement.DAL.Models.Enums;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.BLL.Services.Classes;

public class TrainerService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ITrainerService
{
    public async Task<Result<IEnumerable<TrainerIndexVM>>> GetAllAsync(CancellationToken ct = default)
    {
        var trainers = await unitOfWork.Traineers.GetAllAsync(ct);
        if (trainers == null)
            return Result<IEnumerable<TrainerIndexVM>>.Failure("No Trainers Found");
        return Result<IEnumerable<TrainerIndexVM>>.Success(mapper.Map<IEnumerable<TrainerIndexVM>>(trainers));
    }

    public async Task<Result> CreateAsync(CreateTrainerVM model, CancellationToken ct = default)
    {
        var email = model.Email.Trim().ToLowerInvariant();
        var phone = model.Phone.Trim();
        var name = model.Name.Trim();

        if (await unitOfWork.Traineers.IsEmailTakenAsync(email, null, ct))
            return Result.Failure("This email is already registered.", nameof(model.Email));

        if (await unitOfWork.Traineers.IsPhoneTakenAsync(phone, null, ct))
            return Result.Failure("This phone number is already registered.", nameof(model.Phone));

        if (!Enum.TryParse(model.Gender, true, out Gender gender))
            return Result.Failure("Invalid gender.", nameof(model.Gender));


        await unitOfWork.Traineers.AddAsync(mapper.Map<Traineer>(model), ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }

    public async Task<Result<TrainerDetailsVM>> GetDetailsAsync(int id, CancellationToken ct = default)
    {
        var trainer = await unitOfWork.Traineers.GetByIdAsync(id, ct: ct);

        if (trainer is null)
            return Result<TrainerDetailsVM>.Failure("Trainer not Found.", nameof(id));
        return Result<TrainerDetailsVM>.Success(mapper.Map<TrainerDetailsVM>(trainer));

    }

    public async Task<Result<UpdateTrainerVM>> GetForUpdateAsync(int id, CancellationToken ct = default)
    {
        var trainer = await unitOfWork.Traineers.GetByIdAsync(id, ct: ct);
        if (trainer is null)
            return Result<UpdateTrainerVM>.Failure("Trainer not Found", nameof(id));
        return Result<UpdateTrainerVM>.Success(mapper.Map<UpdateTrainerVM>(trainer));
    }

    public async Task<Result> UpdateAsync(int id, UpdateTrainerVM model, CancellationToken ct = default)
    {
        var trainer = await unitOfWork.Traineers.GetByIdAsync(id, ct: ct);

        if (trainer is null)
            return Result.Failure("Trainer not found.", nameof(id));

        if (!string.Equals(trainer.Name, model.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            return Result.Failure("Name cannot be changed.", nameof(model.Name));

        var normalizedEmail = model.Email.Trim().ToLowerInvariant();
        var normalizedPhone = model.Phone.Trim();

        if (await unitOfWork.Traineers.IsEmailTakenAsync(normalizedEmail, id, ct))
            return Result.Failure("This email is already registered.", nameof(model.Email));

        if (await unitOfWork.Traineers.IsPhoneTakenAsync(normalizedPhone, id, ct))
            return Result.Failure("This phone number is already registered.", nameof(model.Phone));

        mapper.Map(model, trainer);
        await unitOfWork.Traineers.UpdateAsync(trainer, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();


    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var trainer = await unitOfWork.Traineers.GetByIdAsync(id, ct: ct);

        if (trainer is null)
            return Result.Failure("Trainer not found.", nameof(id));

        await unitOfWork.Traineers.SoftDeleteAsync(trainer, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}
