using RealEstate.Domain.Entities;
using Xunit;

namespace RealEstate.Domain.Tests.Entities;

public class OwnerTests
{
    [Fact]
    public void CreateOwner_WithValidData_ShouldSucceed()
    {
        // Arrange
        var name = "John Doe";
        var address = "123 Main St";
        var photo = "photo.jpg";
        var birthday = new DateTime(1980, 5, 15);

        // Act
        var owner = new Owner
        {
            Name = name,
            Address = address,
            Photo = photo,
            Birthday = birthday
        };

        // Assert
        Assert.NotNull(owner);
        Assert.Equal(name, owner.Name);
        Assert.Equal(address, owner.Address);
        Assert.Equal(photo, owner.Photo);
        Assert.Equal(birthday, owner.Birthday);
        Assert.Null(owner.Id); // Id debería ser nulo inicialmente
    }

    [Fact]
    public void Owner_WithNullProperties_ShouldBeAllowed()
    {
        // Act
        var owner = new Owner();

        // Assert
        Assert.NotNull(owner);
        Assert.Null(owner.Id);
        Assert.Null(owner.Name);
        Assert.Null(owner.Address);
        Assert.Null(owner.Photo);
        Assert.Equal(default, owner.Birthday);
    }

    [Fact]
    public void Owner_WithEmptyStringProperties_ShouldBeAllowed()
    {
        // Act
        var owner = new Owner
        {
            Name = "",
            Address = "",
            Photo = ""
        };

        // Assert
        Assert.NotNull(owner);
        Assert.Equal("", owner.Name);
        Assert.Equal("", owner.Address);
        Assert.Equal("", owner.Photo);
    }
}