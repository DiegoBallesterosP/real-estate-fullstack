using Moq;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using Xunit;

namespace RealEstate.Domain.Tests.Repositories;

public class IPropertyRepositoryTests
{
    [Fact]
    public async Task PropertyRepository_GetByIdAsync_ShouldReturnProperty()
    {
        // Arrange
        var mockRepo = new Mock<IPropertyRepository>();
        var expectedProperty = new Property
        {
            Id = "prop-123",
            Name = "Test Property",
            Price = 250000m
        };
        
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedProperty);

        // Act
        var result = await mockRepo.Object.GetByIdAsync("prop-123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("prop-123", result.Id);
        Assert.Equal("Test Property", result.Name);
        Assert.Equal(250000m, result.Price);
        mockRepo.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task PropertyRepository_GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var mockRepo = new Mock<IPropertyRepository>();
        
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Property?)null);

        // Act
        var result = await mockRepo.Object.GetByIdAsync("invalid-id");

        // Assert
        Assert.Null(result);
        mockRepo.Verify(repo => repo.GetByIdAsync(It.IsAny<string>()), Times.Once);
    }
}