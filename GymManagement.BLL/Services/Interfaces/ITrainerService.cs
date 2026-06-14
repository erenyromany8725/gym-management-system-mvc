using GymManagement.BLL.Common;
using GymManagement.BLL.ViewModel.HealthRecord;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.BLL.ViewModel.Trainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Services.Interfaces;

public interface ITrainerService
{
    Task<Result<IEnumerable<TrainerIndexVM>>> GetAllAsync(CancellationToken ct = default);
    Task<Result> CreateAsync(CreateTrainerVM model, CancellationToken ct = default);
    Task<Result<TrainerDetailsVM>> GetDetailsAsync(int id, CancellationToken ct = default);
    Task<Result<UpdateTrainerVM>> GetForUpdateAsync(int id, CancellationToken ct = default);
    Task<Result> UpdateAsync(int id, UpdateTrainerVM model, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}
