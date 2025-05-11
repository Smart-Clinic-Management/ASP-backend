using Microsoft.EntityFrameworkCore;
using SmartClinic.Infrastructure.Data;
using System;

namespace SmartClinic.Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public ApplicationDbContext Context { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new ApplicationDbContext(options);
        Context.Database.EnsureCreated();

        // Seed test data if needed
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        // Add seed data for testing as needed
        // Example: Context.Doctors.AddRange(new List<Doctor> { ... });
        // Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}