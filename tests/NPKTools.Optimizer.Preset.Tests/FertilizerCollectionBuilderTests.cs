using NPKTools.Core.Domain.Fertilizers;
using NPKTools.Core.Domain.Fertilizers.Builders;
using Xunit;

namespace NPKTools.Optimizer.Preset.Tests;

public class FertilizerCollectionBuilderTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public void Add_SingleFertilizer_AddsFertilizerCorrectly()
    {
        // Arrange
        FertilizerCollectionBuilder builder = new FertilizerCollectionBuilder();
        Fertilizer fertilizerResultModel = new FertilizerBuilder().AddK(38.672).AddNo3(13.854).Build();

        // Act
        builder.Add(fertilizerResultModel);
        IList<Fertilizer> result = builder.Build();

        // Assert
        Assert.Single(result);
        Assert.Equal(fertilizerResultModel, result.First());
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Add_DuplicateFertilizer_ThrowsInvalidOperationException()
    {
        // Arrange
        FertilizerCollectionBuilder builder = new FertilizerCollectionBuilder();
        Fertilizer fertilizerResultModel = new FertilizerBuilder().AddK(38.672).AddNo3(13.854).Build();
        builder.Add(fertilizerResultModel);  

        // Act 
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => builder.Add(fertilizerResultModel));
    
        //Assert
        Assert.Equal("Duplicate fertilizer detected with identical attributes.", ex.Message);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void Build_MultipleFertilizers_BuildsCorrectCollection()
    {
        // Arrange
        FertilizerCollectionBuilder builder = new FertilizerCollectionBuilder();
        Fertilizer fert1 = new FertilizerBuilder().AddK(38.672).AddNo3(13.854).Build();
        Fertilizer fert2 = new FertilizerBuilder().AddCaNonChelated(16.972).AddNo3(11.863).Build();

        // Act
        builder.Add(fert1).Add(fert2);
        IList<Fertilizer> result = builder.Build();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(fert1, result);
        Assert.Contains(fert2, result);
    }
}