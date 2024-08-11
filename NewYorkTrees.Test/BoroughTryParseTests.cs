using NewYorkTrees.DAL.Models;
using Shouldly;

namespace NewYorkTrees.Test;

public class BoroughTryParseTests
{
    [Fact]
    public void GivenBronx_ShouldReturnBronxBorough()
    {
        // Arrange
        var input = "Bronx";

        // Act
        var result = Borough.TryParse(input, out var output);
        
        // Assert
        result.ShouldBeTrue();
        output.ShouldNotBeNull();
        output.ShouldBe(Borough.Bronx);
    }
    
    [Fact]
    public void GivenBrooklyn_ShouldReturnBrooklynBorough()
    {
        // Arrange
        var input = "Brooklyn";

        // Act
        var result = Borough.TryParse(input, out var output);
        
        // Assert
        result.ShouldBeTrue();
        output.ShouldNotBeNull();
        output.ShouldBe(Borough.Brooklyn);
    }
    
    [Fact]
    public void GivenManhattan_ShouldReturnManhattanBorough()
    {
        // Arrange
        var input = "Manhattan";

        // Act
        var result = Borough.TryParse(input, out var output);
        
        // Assert
        result.ShouldBeTrue();
        output.ShouldNotBeNull();
        output.ShouldBe(Borough.Manhattan);
    }
    
    [Fact]
    public void GivenQueens_ShouldReturnQueensBorough()
    {
        // Arrange
        var input = "Queens";

        // Act
        var result = Borough.TryParse(input, out var output);
        
        // Assert
        result.ShouldBeTrue();
        output.ShouldNotBeNull();
        output.ShouldBe(Borough.Queens);
    }
    
    [Fact]
    public void GivenStatenIsland_ShouldReturnStatenIslandBorough()
    {
        // Arrange
        var input = "Staten Island";

        // Act
        var result = Borough.TryParse(input, out var output);
        
        // Assert
        result.ShouldBeTrue();
        output.ShouldNotBeNull();
        output.ShouldBe(Borough.StatenIsland);
    }
    
    [Fact]
    public void GivenTheOtherStatenIsland_ShouldReturnStatenIslandBorough()
    {
        // Arrange
        var input = "StatenIsland";

        // Act
        var result = Borough.TryParse(input, out var output);
        
        // Assert
        result.ShouldBeTrue();
        output.ShouldNotBeNull();
        output.ShouldBe(Borough.StatenIsland);
    }
}