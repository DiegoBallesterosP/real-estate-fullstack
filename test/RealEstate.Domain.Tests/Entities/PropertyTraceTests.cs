using RealEstate.Domain.Entities;
using Xunit;

namespace RealEstate.Domain.Tests.Entities;

public class PropertyTraceTests
{
    [Fact]
    public void CreatePropertyTrace_WithValidData_ShouldSucceed()
    {
        // Arrange
        var dateSale = DateTime.Now;
        var name = "Sale Transaction";
        var value = 250000m;
        var tax = 25000m;

        // Act
        var propertyTrace = new PropertyTrace
        {
            DateSale = dateSale,
            Name = name,
            Value = value,
            Tax = tax
        };

        // Assert
        Assert.NotNull(propertyTrace);
        Assert.Equal(dateSale, propertyTrace.DateSale);
        Assert.Equal(name, propertyTrace.Name);
        Assert.Equal(value, propertyTrace.Value);
        Assert.Equal(tax, propertyTrace.Tax);
        Assert.Null(propertyTrace.Id);
    }

    [Fact]
    public void PropertyTrace_WithNegativeValues_ShouldBeAllowed()
    {
        // Act
        var propertyTrace = new PropertyTrace
        {
            Value = -1000m,
            Tax = -100m
        };

        // Assert
        Assert.Equal(-1000m, propertyTrace.Value);
        Assert.Equal(-100m, propertyTrace.Tax);
    }

    [Fact]
    public void PropertyTrace_WithNullName_ShouldBeAllowed()
    {
        // Act
        var propertyTrace = new PropertyTrace
        {
            Name = null
        };

        // Assert
        Assert.Null(propertyTrace.Name);
    }
}