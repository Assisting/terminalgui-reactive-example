using NewYorkTrees.DAL.Models;

namespace NewYorkTrees.DAL.HttpClients;

public interface INewYorkTreeDataClient
{
    public Task<IEnumerable<NewYorkTree>?> GetTreesForBorough(Borough boroughToSearch, int limit = 10, int pageIndex = 0);
}