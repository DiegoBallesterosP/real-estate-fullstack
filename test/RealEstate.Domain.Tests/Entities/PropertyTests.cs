using RealEstate.Domain.Entities;
using Xunit;

namespace RealEstate.Domain.Tests.Entities;

public class PropertyTests
{
    [Fact]
    public void CreateProperty_WithValidData_ShouldSucceed()
    {
        // Arrange
        var name = "Beautiful Villa";
        var address = "123 Main St, City";
        var price = 250000m;
        var codeInternal = "PROP-001";
        var year = 2020;
        var ownerId = "owner-123";

        // Act
        var property = new Property
        {
            Name = name,
            Address = address,
            Price = price,
            CodeInternal = codeInternal,
            Year = year,
            OwnerId = ownerId
        };

        // Assert
        Assert.NotNull(property);
        Assert.Equal(name, property.Name);
        Assert.Equal(address, property.Address);
        Assert.Equal(price, property.Price);
        Assert.Equal(codeInternal, property.CodeInternal);
        Assert.Equal(year, property.Year);
        Assert.Equal(ownerId, property.OwnerId);
        Assert.NotNull(property.Images);
        Assert.NotNull(property.Traces);
        Assert.Empty(property.Images);
        Assert.Empty(property.Traces);
        Assert.Null(property.Id);
    }

    [Fact]
    public void Property_WithNegativePrice_ShouldBeAllowed()
    {
        // Act
        var property = new Property
        {
            Price = -1000m // Precio negativo
        };

        // Assert
        Assert.Equal(-1000m, property.Price);
    }

    [Fact]
    public void Property_AddImage_ShouldWorkCorrectly()
    {
        // Arrange
        var property = new Property();
        var image = new PropertyImage { File = "image1.jpg", Enabled = true };

        // Act
        property.Images.Add(image);

        // Assert
        Assert.Single(property.Images);
        Assert.Equal("image1.jpg", property.Images[0].File);
        Assert.True(property.Images[0].Enabled);
    }

    [Fact]
    public void Property_AddTrace_ShouldWorkCorrectly()
    {
        // Arrange
        var property = new Property();
        var trace = new PropertyTrace 
        { 
            Name = "Sale", 
            Value = 250000m, 
            Tax = 25000m,
            DateSale = DateTime.Now
        };

        // Act
        property.Traces.Add(trace);

        // Assert
        Assert.Single(property.Traces);
        Assert.Equal("Sale", property.Traces[0].Name);
        Assert.Equal(250000m, property.Traces[0].Value);
        Assert.Equal(25000m, property.Traces[0].Tax);
    }
}