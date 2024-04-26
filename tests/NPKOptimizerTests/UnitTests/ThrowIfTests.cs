using NPKOptimizer.Common;
using Xunit;

namespace NPKOptimizer.Tests.UnitTests;

public class ThrowIfTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public void Default_WithDefaultValue_ThrowsArgumentException()
    {
        Guid defaultValue = default;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => ThrowIf.Default(defaultValue, nameof(defaultValue)));
        Assert.StartsWith("Value cannot be the default value.", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Default_WithNonDefaultValue_DoesNotThrow()
    {
        Guid nonDefaultValue = Guid.NewGuid();

        Exception exceptionRecord = Record.Exception(() => ThrowIf.Default(nonDefaultValue, nameof(nonDefaultValue)));
        Assert.Null(exceptionRecord);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GreaterThan_WithValueGreaterThanMax_ThrowsArgumentException()
    {
        double value = 101;
        double max = 100;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => ThrowIf.GreaterThan(value, max, nameof(value)));
        Assert.StartsWith("Value cannot be greater than 100.", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GreaterThan_WithValueEqualToMax_DoesNotThrow()
    {
        double value = 100;
        double max = 100;

        Exception exceptionRecord = Record.Exception(() => ThrowIf.GreaterThan(value, max, nameof(value)));
        Assert.Null(exceptionRecord);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void LowerThan_WithValueLowerThanMin_ThrowsArgumentException()
    {
        double value = -1;
        double min = 0;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => ThrowIf.LowerThan(value, min, nameof(value)));
        Assert.Equal("Value cannot be lower than 0. (Parameter 'value')", exception.Message);
    }


    [Fact]
    [Trait("Category", "Unit")]
    public void LowerThan_WithValueEqualToMin_DoesNotThrow()
    {
        double value = 0;
        double min = 0;

        Exception exceptionRecord = Record.Exception(() => ThrowIf.LowerThan(value, min, nameof(value)));
        Assert.Null(exceptionRecord);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void NotInRange_WithValueNotInRange_ThrowsArgumentOutOfRangeException()
    {
        double value = 101;
        double min = 1;
        double max = 100;

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.NotInRange(value, min, max, nameof(value)));
        Assert.StartsWith("Value must be between 1 and 100.", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }


    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(1)] 
    [InlineData(100)] 
    [InlineData(50)] 
    public void NotInRange_WithValuesInRange_DoesNotThrow(double value)
    {
        double min = 1;
        double max = 100;

        Exception exceptionRecord = Record.Exception(() => ThrowIf.NotInRange(value, min, max, nameof(value)));
        Assert.Null(exceptionRecord);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void NotEmpty_WithNullCollection_ThrowsArgumentNullException()
    {
        IEnumerable<int> nullCollection = null;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ThrowIf.NullOrEmpty(nullCollection, nameof(nullCollection)));
        Assert.StartsWith("The collection cannot be null.", exception.Message);
        Assert.Equal("nullCollection", exception.ParamName);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void NotEmpty_WithEmptyCollection_ThrowsArgumentException()
    {
        List<int> emptyCollection = new List<int>();

        ArgumentException exception = Assert.Throws<ArgumentException>(() => ThrowIf.NullOrEmpty(emptyCollection, nameof(emptyCollection)));
        Assert.StartsWith("The collection cannot be empty.", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void NotEmpty_WithNonEmptyCollection_DoesNotThrow()
    {
        List<int> nonEmptyCollection = new List<int> { 1, 2, 3 };

        Exception exceptionRecord = Record.Exception(() => ThrowIf.NullOrEmpty(nonEmptyCollection, nameof(nonEmptyCollection)));
        Assert.Null(exceptionRecord);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void LowerThanOrEqual_WithValueLowerThanOrEqualMin_ThrowsArgumentException()
    {
        double value = 0;
        double min = 0;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => ThrowIf.LowerThanOrEqual(value, min, nameof(value)));
        Assert.Equal($"Value must be greater than {min}. (Parameter 'value')", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void LowerThanOrEqual_WithValueGreaterThanMin_DoesNotThrow()
    {
        double value = 1;
        double min = 0;

        Exception exceptionRecord = Record.Exception(() => ThrowIf.LowerThanOrEqual(value, min, nameof(value)));
        Assert.Null(exceptionRecord);
    }
}