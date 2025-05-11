using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
using SmartClinic.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SmartClinic.Tests.Fixtures;

public class MockRepositoryFixture
{
    public Mock<IUnitOfWork> MockUnitOfWork { get; }

    // Dictionary to store mocked repositories for different entity types
    private readonly Dictionary<Type, object> _mockRepos = new();

    public MockRepositoryFixture()
    {
        MockUnitOfWork = new Mock<IUnitOfWork>();
    }

    // Generic method to get or create mock repository for a specific entity type
    public Mock<IGenericRepo<T>> GetMockRepo<T>() where T : BaseEntity
    {
        var entityType = typeof(T);

        if (!_mockRepos.TryGetValue(entityType, out var repo))
        {
            repo = new Mock<IGenericRepo<T>>();
            _mockRepos[entityType] = repo;

            // Set up the UnitOfWork to return this repository
            MockUnitOfWork
                .Setup(u => u.Repo<T>())
                .Returns(((Mock<IGenericRepo<T>>)repo).Object);
        }

        return (Mock<IGenericRepo<T>>)repo;
    }
}