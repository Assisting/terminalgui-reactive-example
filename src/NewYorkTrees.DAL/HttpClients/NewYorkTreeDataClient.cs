using System.Net.Http.Json;
using NewYorkTrees.DAL.Models;
using SODA;

namespace NewYorkTrees.DAL.HttpClients;

public class NewYorkTreeDataClient : INewYorkTreeDataClient
{
    private readonly HttpClient httpClient;

    public NewYorkTreeDataClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<NewYorkTree>?> GetTreesForBorough(Borough boroughToSearch, int limit = 20, int pageIndex = 0)
    {
        var searchQuery = $"?boroname={boroughToSearch}&$limit={limit}&$offset={pageIndex}";
        return await this.httpClient.GetFromJsonAsync<IEnumerable<NewYorkTree>>(searchQuery);
    }
}