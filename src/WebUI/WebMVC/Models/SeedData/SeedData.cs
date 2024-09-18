using Persistence.ContextModel;

namespace WebMVC.Models.SeedData;

public static class ProjectSeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
    }
}

