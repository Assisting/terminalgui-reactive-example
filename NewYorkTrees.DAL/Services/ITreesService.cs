using NewYorkTrees.DAL.Models;

namespace NewYorkTrees.DAL.Services;

public interface ITreesService
{
    public Task<IEnumerable<NewYorkTree>?> GetBoroughTrees(Borough borough, int page);
}