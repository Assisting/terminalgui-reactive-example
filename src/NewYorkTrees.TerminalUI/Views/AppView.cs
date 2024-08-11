using System.Reactive.Disposables;
using NewYorkTrees.ViewModels;
using ReactiveUI;
using Terminal.Gui;

namespace NewYorkTrees.TerminalUI.Views;

public sealed class AppView : Window, IViewFor<AppViewModel>
{
    public AppViewModel? ViewModel { get; set; }
    
    public AppView(AppViewModel viewModel)
    {
        this.ViewModel = viewModel;
        
        var searchBarFrame = new SearchBarView(this.ViewModel.SearchBarViewModel);
        this.Add(searchBarFrame);

        var treeResultsFrame = new TreeResultsView(this.ViewModel.TreeResultsViewModel);
        this.Add(treeResultsFrame);

        this.Title = "New York Trees Search";
        this.Width = Dim.Fill();
        this.Height = Dim.Fill();
        this.Border.BorderStyle = LineStyle.Single;
    }
    
    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (AppViewModel?)value;
    }
    
    private readonly CompositeDisposable disposable = [];

    protected override void Dispose (bool disposing)
    {
        disposable.Dispose ();
        base.Dispose (disposing);
    }
}