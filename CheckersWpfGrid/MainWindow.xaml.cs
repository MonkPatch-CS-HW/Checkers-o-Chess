using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Documents;
using CheckersWpfGrid.MoveStrategy;
using CheckersWpfGrid.MoveStrategy.Chess;
using CheckersWpfGrid.MoveStrategy.RussianCheckers;

namespace CheckersWpfGrid;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Ruleset> RuleSets { get; } = new List<Ruleset>()
    {
        new RussianCheckersRuleset(),
        new ChessRuleset(),
    };

    public Dictionary<string, List<Style>> Styles { get; } = new()
    {
        ["Russian checkers"] =
            new List<Style> { new("Default", "Themes/RussianCheckers/Default.xaml") },
        ["Chess"] = new List<Style> { new("Default", "Themes/Chess/Default.xaml") },
    };

    public List<Style> AvailableStyles
    {
        get => (List<Style>)GetValue(AvailableStylesProperty);
        set => SetValue(AvailableStylesProperty, value);
    }

    public static readonly DependencyProperty AvailableStylesProperty = DependencyProperty.Register(
        nameof(AvailableStyles),
        typeof(List<Style>),
        typeof(MainWindow));

    public Ruleset RuleSet { get; set; }

    public Style SelectedStyle
    {
        get => (Style)GetValue(SelectedStyleProperty);
        set => SetValue(SelectedStyleProperty, value);
    }

    public static readonly DependencyProperty SelectedStyleProperty = DependencyProperty.Register(
        nameof(SelectedStyle),
        typeof(Style),
        typeof(MainWindow));

    public bool WithBot { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        RuleSet = RuleSets[0];
        UpdateStyles();
    }

    public void UpdateStyles(object? sender = null, RoutedEventArgs? e = null)
    {
        AvailableStyles = Styles[RuleSet.Name];
        SelectedStyle = AvailableStyles[0];
    }

    private void OnCreateButtonClick(object sender, RoutedEventArgs e)
    {
        var game = new Game(RuleSet, WithBot);
        var renderer = new Renderer(game, SelectedStyle);
        renderer.Show();
    }
}