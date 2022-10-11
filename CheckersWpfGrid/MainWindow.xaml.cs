using System.Windows;
using CheckersWpfGrid.MoveStrategy.RussianChess;

namespace CheckersWpfGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game Game { get; }

        public MainWindow()
        {
            InitializeComponent();
            var ruleset = new RussianChessRuleset();
            Game = new Game(ruleset);
            Renderer.Render(Game);
        }
    }
}