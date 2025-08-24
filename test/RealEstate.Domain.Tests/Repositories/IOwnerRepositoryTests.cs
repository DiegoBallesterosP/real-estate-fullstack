using Moq;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using Xunit;

namespace RealEstate.Domain.Tests.Repositories;

public class IOwnerRepositoryTests
{
    [Fact]
    public async Task OwnerRepository_GetByIdAsync_ShouldReturnOwner()
    {
        // Arrange
        var mockRepo = new Mock<IOwnerRepository>();
        var expectedOwner = new Owner
        {
            Id = "owner-123",
            Name = "John Doe"
        };
        
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedOwner);

        // Act
        var result = await mockRepo.Object.GetByIdAsync("owner-123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("owner-123", result.Id);
        Assert.Equal("John Doe", result.Name);
        mockRepo.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once);
    }
}