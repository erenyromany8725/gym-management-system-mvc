using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Seed;

public static class CategorySeeder
{
    public static async Task SeederAsync(GymDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;
        List<Category> categories =
        [
             new Category{Name = "Cardio" },
                new Category{Name = "Strength" },
                new Category{Name = "Yoga" },
                new Category{Name = "Boxing" }, 
                new Category {Name = "Crossfit" }
        ];
    }
}
