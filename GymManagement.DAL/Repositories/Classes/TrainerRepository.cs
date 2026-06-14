using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes;

public class TrainerRepository(GymDbContext context) : Repository<Traineer>(context), ITrainerRepository

{
    private readonly GymDbContext _dbContext = context;

    public Task<bool> IsEmailTakenAsync(string normalizedEmail, int? execludedId = null, CancellationToken ct = default)
    => _dbContext.Set<Traineer>().AnyAsync(t => t.Email == normalizedEmail && (!execludedId.HasValue || t.Id != execludedId.Value), ct);

    public Task<bool> IsPhoneTakenAsync(string normalizedPhone, int? execludedId = null, CancellationToken ct = default)
       => _dbContext.Set<Traineer>().AnyAsync(t => !t.IsDeleted && t.Phone == normalizedPhone && (!execludedId.HasValue || t.Id != execludedId.Value), ct);


}
