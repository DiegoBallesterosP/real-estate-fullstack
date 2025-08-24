using RealEstate.Domain.Entities;
using Xunit;

namespace RealEstate.Domain.Tests.Entities;

public class PropertyImageTests
{
    [Fact]
    public void CreatePropertyImage_WithValidData_ShouldSucceed()
    {
        // Arrange
        var file = "images/villa.jpg";
        var enabled = true;

        // Act
        var propertyImage = new PropertyImage
        {
            File = file,
            Enabled = enabled
        };

        // Assert
        Assert.NotNull(propertyImage);
        Assert.Equal(file, propertyImage.File);
        Assert.Equal(enabled, propertyImage.Enabled);
        Assert.Null(propertyImage.Id);
    }

    [Fact]
    public void PropertyImage_WithNullFile_ShouldBeAllowed()
    {
        // Act
        var propertyImage = new PropertyImage
        {
            File = null,
            Enabled = false
        };

        // Assert
        Assert.Null(propertyImage.File);
        Assert.False(propertyImage.Enabled);
    }

    [Fact]
    public void PropertyImage_WithEmptyFile_ShouldBeAllowed()
    {
        // Act
        var propertyImage = new PropertyImage
        {
            File = "",
            Enabled = true
        };

        // Assert
        Assert.Equal("", propertyImage.File);
        Assert.True(propertyImage.Enabled);
    }
}