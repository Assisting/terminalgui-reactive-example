using System;
using Terminal.Gui;

namespace NewYorkTrees.TerminalUI;

public static class ViewExtensions
{
    public static (View MainView, TOut LastControl) AddControl<TOut> (this View view, Action<TOut> action)
        where TOut : View, new()
    {
        TOut result = new ();
        action (result);
        view.Add (result);
        return (view, result);
    }

    public static (View MainView, TOut LastControl) AddControlAfter<TOut> (this (View MainView, View LastControl) view, Action<View, TOut> action)
        where TOut : View, new()
    {
        TOut result = new ();
        action (view.LastControl, result);
        view.MainView.Add (result);
        return (view.MainView, result);
    }
}