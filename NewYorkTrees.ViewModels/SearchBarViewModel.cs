using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NewYorkTrees.ViewModels;

public class SearchBarViewModel
{
    public readonly AppViewModel Parent;
    
    [Reactive] public bool SearchError { get; set; }
    
    [Reactive] public string SearchQuery { get; set; }

    public SearchBarViewModel(AppViewModel parent)
    {
        this.Parent = parent;
        this.SearchQuery = string.Empty;
    }
}