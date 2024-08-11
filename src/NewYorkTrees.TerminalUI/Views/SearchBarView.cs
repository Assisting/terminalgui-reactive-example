using System.Reactive.Disposables;
using System.Reactive.Linq;
using NewYorkTrees.ViewModels;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Terminal.Gui;

namespace NewYorkTrees.TerminalUI.Views;

public class SearchBarView : FrameView, IViewFor<SearchBarViewModel>
{
    public SearchBarViewModel? ViewModel { get; set; }
    
    public SearchBarView(SearchBarViewModel viewModel)
    {
        this.ViewModel = viewModel;

        this.Title = "Search by Borough";
        this.Width = Dim.Percent(40);
        this.Height = 6;
        this.X = 2;
        this.Y = 2;

        this.ViewModel
            .WhenAnyValue(vm => vm.SearchError)
            .Subscribe(se =>
            {
                if (se)
                {
                    MessageBox.ErrorQuery("Search Error",
                        "\nPlease check the contents of your search field and try again.\n",
                        "Ok");
                    this.ViewModel.SearchError = false;
                }
            })
            .DisposeWith(this.disposable);

        var searchBar = this.AddControl<TextField>(
            textField =>
            {
                textField.X = 1;
                textField.Y = 1;
                var boroughNameGenerator = new SingleWordSuggestionGenerator();
                boroughNameGenerator.AllSuggestions = new List<string>
                {
                    "Bronx", "Brooklyn", "Manhattan", "Queens", "Staten Island"
                };
                textField.Autocomplete.SuggestionGenerator = boroughNameGenerator;
                textField.Autocomplete.MaxWidth = 13;
                textField.Width = Dim.Percent(50);

                this.ViewModel
                    .WhenAnyValue(vm => vm.SearchQuery)
                    .BindTo(textField, tf => tf.Text)
                    .DisposeWith(this.disposable);
                
                textField
                    .Events()
                    .TextChanged
                    .Select(_ => textField.Text)
                    .DistinctUntilChanged()
                    .BindTo(this.ViewModel, vm => vm.SearchQuery)
                    .DisposeWith(this.disposable);
            });

        searchBar.AddControlAfter<Button>(
            (previous, submitButton) =>
            {
                submitButton.Text = "Search";
                submitButton.X = previous.X;
                submitButton.Y = Pos.Bottom(previous);

                submitButton
                    .Events()
                    .Accept
                    .Select(_ => this.ViewModel.SearchQuery)
                    .InvokeCommand(this.ViewModel, vm => vm.Parent.SearchForTreesCommand)
                    .DisposeWith(this.disposable);
            });
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (SearchBarViewModel?)value;
    }
    
    private readonly CompositeDisposable disposable = [];

    protected override void Dispose (bool disposing)
    {
        disposable.Dispose ();
        base.Dispose (disposing);
    }
}