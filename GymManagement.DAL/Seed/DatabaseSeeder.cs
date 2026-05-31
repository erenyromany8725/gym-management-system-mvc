using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(GymDbContext dbContext)
    {
        await CategorySeeder.SeederAsync(dbContext);
    }
}
