using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using NewYorkTrees.DAL.HttpClients;
using NewYorkTrees.DAL.Models;
using Shouldly;

namespace NewYorkTrees.Test;

[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public class NewYorkTreeDataClientTests
{
    private readonly INewYorkTreeDataClient client;
    
    public NewYorkTreeDataClientTests()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var newYorkOpenDataEndpoint = config.GetConnectionString("NewYorkOpenDataEndpoint");
        if (newYorkOpenDataEndpoint is null)
        {
            throw new NullReferenceException();
        }
        var baseClient = new HttpClient();
        baseClient.BaseAddress = new Uri(newYorkOpenDataEndpoint);
        this.client = new NewYorkTreeDataClient(baseClient);
    }
    
    [Fact]
    public async Task GivenTypedClient_ShouldRetrieveTrees()
    {
        // Arrange
        
        // Act
        var result = await this.client.GetTreesForBorough(Borough.Queens, 5);
        
        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(5);
    }
    
    
    
    [Fact]
    public async Task GivenTypedClient_ShouldBeAbleToPage()
    {
        // Arrange
        
        // Act
        var firstResult = await this.client.GetTreesForBorough(Borough.Bronx, 5);
        var secondResult = await this.client.GetTreesForBorough(Borough.Bronx, 5, 5);
        
        // Assert
        firstResult.ShouldNotBeNull();
        secondResult.ShouldNotBeNull();
        firstResult.Count().ShouldBe(5);
        secondResult.Count().ShouldBe(5);
        firstResult.All(ftr =>
        {
            return !secondResult.Select(str => str.TreeId).Contains(ftr.TreeId);
        }).ShouldBeTrue();
    }
}