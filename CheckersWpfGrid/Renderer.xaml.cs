using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace CheckersWpfGrid;

public partial class Renderer : UserControl
{
    public Renderer()
    {
        InitializeComponent();
    }

    public void Render(Game game)
    {
        Reset();
        FillRowsAndColumns();
        RenderTable(game.Table);
        BindTable(game.Table);
        RenderBoard(game.Board);
        BindFigures(game.Board);
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

    private void Reset()
    {
        Grid.Children.Clear();
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
        }
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
        }
    }
}