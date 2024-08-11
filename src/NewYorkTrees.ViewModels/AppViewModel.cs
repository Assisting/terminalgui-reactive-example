using System.Reactive;
using NewYorkTrees.DAL.Models;
using NewYorkTrees.DAL.Services;
using ReactiveUI;

namespace NewYorkTrees.ViewModels;

public class AppViewModel
{
    private readonly ITreesService treesService;

    public readonly SearchBarViewModel SearchBarViewModel;
    public readonly TreeResultsViewModel TreeResultsViewModel;

    private Borough LastBoroughSearched { get; set; }
    
    public AppViewModel(ITreesService treesService)
    {
        this.treesService = treesService;

        this.SearchBarViewModel = new SearchBarViewModel(this);
        this.TreeResultsViewModel = new TreeResultsViewModel(this);
        
        this.SearchForTreesCommand = ReactiveCommand.CreateFromTask<string, Unit>(
            async (query, _) =>
            {
                var canParse = Borough.TryParse(query, out var borough);
                if (!canParse || borough is null)
                {
                    this.SearchBarViewModel.SearchError = true;
                }
                else
                {
                    this.LastBoroughSearched = borough;
                    this.TreeResultsViewModel.TreeSearchResults = await this.treesService.GetBoroughTrees(borough, 0);
                }

                return Unit.Default;
            });
        
        this.GetSearchResultsPageCommand = ReactiveCommand.Create<int, Unit>(
            page =>
            {
                if (this.LastBoroughSearched is null)
                {
                    this.SearchBarViewModel.SearchError = true;
                }
                else
                {
                    this.treesService.GetBoroughTrees(this.LastBoroughSearched, page);
                }

                return Unit.Default;
            });
    }
    
    public ReactiveCommand<string, Unit> SearchForTreesCommand { get; }
    
    public ReactiveCommand<int, Unit> GetSearchResultsPageCommand { get; }
}