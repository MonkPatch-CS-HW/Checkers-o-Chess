using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Documents;
using CheckersWpfGrid.MoveStrategy;
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
    };

    public Ruleset RuleSet { get; set; }
    
    public bool WithBot { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        RuleSet = RuleSets[0];
    }

    private void OnCreateButtonClick(object sender, RoutedEventArgs e)
    {
        var game = new Game(RuleSet, WithBot);
        var renderer = new Renderer(game);
        renderer.Show();
    }
}