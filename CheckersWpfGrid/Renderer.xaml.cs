using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CheckersWpfGrid;

public partial class Renderer : Window
{
    public Game Game { get; }
    protected Highlighter Highlighter { get; }

    public Renderer(Game game)
    {
        Game = game;
        Highlighter = new Highlighter(Game);
        InitializeComponent();

        FillRowsAndColumns();
        RenderTable(game.Table);
        BindTable(game.Table);
        RenderBoard(game.Board);
        BindFigures(game.Board);

        Highlighter.HighlightGame();
        Game.AfterSelectFigure += _ => Highlighter.HighlightGame();
        Game.AfterMove += _ => Highlighter.HighlightGame();
        Game.AfterWin += winner => MessageBox.Show(winner.Name);
    }

    private void RenderTable(Table table)
    {
        foreach (var cell in table.Cells)
            Grid.Children.Add(cell);
    }

    private void RenderBoard(Board board)
    {
        foreach (var figure in board.Figures.Where(figure => figure != null))
            Grid.Children.Add(figure!);
    }

    private void FillRowsAndColumns()
    {
        for (var i = 0; i < 8; i++)
        {
            Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.RowDefinitions.Add(new RowDefinition());
        }
    }

    private void BindTable(Table table)
    {
        foreach (var cell in table.Cells)
        {
            var source = new RelativeSource(RelativeSourceMode.Self);
            var bindColumn = new Binding("Column") { RelativeSource = source };
            var bindRow = new Binding("Row") { RelativeSource = source };
            cell.SetBinding(Grid.ColumnProperty, bindColumn);
            cell.SetBinding(Grid.RowProperty, bindRow);
            cell.MouseDown += CellOnMouseDown;
        }
    }

    private void CellOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!Game.CurrentPlayer.IsBot)
            Game.MoveActiveFigureTo((Cell)sender);
    }

    private void BindFigures(Board board)
    {
        foreach (var figure in board.Figures)
        {
            if (figure == null)
                continue;
            var source = new RelativeSource(RelativeSourceMode.Self);
            var bindColumn = new Binding("Column") { RelativeSource = source };
            var bindRow = new Binding("Row") { RelativeSource = source };
            figure.SetBinding(Grid.ColumnProperty, bindColumn);
            figure.SetBinding(Grid.RowProperty, bindRow);
            figure.MouseDown += FigureOnMouseDown;
        }
    }

    private void FigureOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!Game.CurrentPlayer.IsBot)
            Game.SelectFigure((Figure)sender);
    }

    private void OnSurrenderBtnClick(object sender, RoutedEventArgs e)
    {
        Game.Surrender(Game.CurrentPlayer);
    }
}