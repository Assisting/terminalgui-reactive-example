using NewYorkTrees.DAL.HttpClients;
using NewYorkTrees.DAL.Models;

namespace NewYorkTrees.DAL.Services;

public class TreesService : ITreesService
{
    private readonly INewYorkTreeDataClient httpClient;
    
    public TreesService(INewYorkTreeDataClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<NewYorkTree>?> GetBoroughTrees(Borough borough, int page = 0)
    {
        return await this.httpClient.GetTreesForBorough(borough, 25, pageIndex: page * 25);
    }
}