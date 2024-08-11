using NewYorkTrees.DAL.Models;
using ReactiveUI.Fody.Helpers;

namespace NewYorkTrees.ViewModels;

public class TreeResultsViewModel
{
    public readonly AppViewModel Parent;
    
    [Reactive] public IEnumerable<NewYorkTree>? TreeSearchResults { get; set; }

    public TreeResultsViewModel(AppViewModel parent)
    {
        this.Parent = parent;
    }
}