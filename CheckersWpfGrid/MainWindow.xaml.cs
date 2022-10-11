using System.Windows;
using CheckersWpfGrid.MoveStrategy.RussianChess;

namespace CheckersWpfGrid;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnCreateButtonClick(object sender, RoutedEventArgs e)
    {
        var ruleset = new RussianChessRuleset();
        var game = new Game(ruleset);
        var renderer = new Renderer(game);
        renderer.Show();
    }
}