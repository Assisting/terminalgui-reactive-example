using System.Reactive.Concurrency;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewYorkTrees.DAL.HttpClients;
using NewYorkTrees.DAL.Services;
using NewYorkTrees.TerminalUI.Views;
using NewYorkTrees.UserPreferences;
using NewYorkTrees.ViewModels;
using ReactiveUI;
using Terminal.Gui;
using ConfigurationManager = Terminal.Gui.ConfigurationManager;

namespace NewYorkTrees.TerminalUI;

class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(140, 45);
        
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var newYorkOpenDataEndpoint = config.GetConnectionString("NewYorkOpenDataEndpoint");
        if (newYorkOpenDataEndpoint is null)
        {
            throw new NullReferenceException();
        }

        var newYorkOpenDataUri = new Uri(newYorkOpenDataEndpoint);
        
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHttpClient<INewYorkTreeDataClient, NewYorkTreeDataClient>(client =>
        {
            client.BaseAddress = newYorkOpenDataUri;
        });
        
        builder.Services.AddTransient<ITreesService, TreesService>();
        using var host = builder.Build();
        
        Application.Init ();
        
        ConfigurationManager.Glyphs.LeftBracket = new Rune('[');
        ConfigurationManager.Glyphs.RightBracket = new Rune(']');
        ConfigurationManager.Glyphs.CheckStateUnChecked = new Rune('╴');
        ConfigurationManager.Glyphs.CheckStateChecked = new Rune('√');

        var userPreferencesHelper = new UserPreferencesHelper();
        ConfigurationManager.Themes!.Theme = userPreferencesHelper.GetUserTheme();
        ConfigurationManager.Apply();
        
        RxApp.MainThreadScheduler = TerminalScheduler.Default;
        RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;
        try
        {
            Application.Run(new AppView(new AppViewModel(host.Services.GetRequiredService<ITreesService>())));
        }
        finally
        {
            Application.Top!.Dispose();
            Application.Shutdown();
        }
    }
}