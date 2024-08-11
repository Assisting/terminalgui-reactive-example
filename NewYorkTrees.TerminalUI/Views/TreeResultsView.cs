using System.Data;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NewYorkTrees.DAL.Models;
using NewYorkTrees.ViewModels;
using ReactiveUI;
using Terminal.Gui;

namespace NewYorkTrees.TerminalUI.Views;

public class TreeResultsView : FrameView, IViewFor<TreeResultsViewModel>
{
    public TreeResultsViewModel? ViewModel { get; set; }
    
    public TreeResultsView(TreeResultsViewModel viewModel)
    {
        this.ViewModel = viewModel;

        this.Title = "Search Results";
        this.X = 2;
        this.Y = 8;
        this.Width = Dim.Percent(90);
        this.Height = Dim.Fill(2);

        this.AddControl<TableView>(
            table =>
            {
                table.X = 1;
                table.Y = 1;
                table.Width = Dim.Fill(1);
                table.Height = Dim.Fill(1);
                table.Table = new DataTableSource(DataTableWithColumns());

                this.ViewModel
                    .Parent
                    .WhenAnyObservable(vm => vm.SearchForTreesCommand.IsExecuting)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Select(_ => this.ViewModel.TreeSearchResults)
                    .Subscribe(
                        data =>
                        {
                            if (data is null)
                            {
                                return;
                            }
                            table.Table = new DataTableSource(this.ConvertEnumerableToDataTable(data));
                        })
                    .DisposeWith(this.disposable);
            });
    }

    private DataTable DataTableWithColumns()
    {
        var table = new DataTable();
        table.Columns.Add("TreeId");
        table.Columns.Add("CreatedAt");
        table.Columns.Add("Status");
        table.Columns.Add("Health");
        table.Columns.Add("Species");
        table.Columns.Add("Borough");
        table.Columns.Add("Address");
        table.Columns.Add("City");
        table.Columns.Add("Zip");
        return table;
    }

    private DataTable ConvertEnumerableToDataTable(IEnumerable<NewYorkTree> trees)
    {
        var table = DataTableWithColumns();
        foreach (var tree in trees)
        {
            table.LoadDataRow([
                tree.TreeId.ToString(), tree.CreatedAt.ToShortDateString(), tree.Status, tree.Health,
                tree.Species, tree.Borough, tree.Address, tree.City, tree.Zip
            ], true);
        }

        return table;
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (TreeResultsViewModel?)value;
    }
    
    private readonly CompositeDisposable disposable = [];

    protected override void Dispose (bool disposing)
    {
        disposable.Dispose ();
        base.Dispose (disposing);
    }
}