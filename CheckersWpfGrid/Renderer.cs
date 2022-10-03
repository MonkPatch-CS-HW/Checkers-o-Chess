using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckersWpfGrid;

public class Renderer
{
    private readonly Grid _grid;
    public Renderer(Grid grid)
    {
        _grid = grid;
    }

    public void Render(Game game)
    {
        Reset();
        FillRowsAndColumns();
        BindCells(game.Cells);
        BindFigures(game.Figures);
        RenderCells(game.Cells);
        RenderFigures(game.Figures);
    }
    
    private void RenderCells(Cell[,] cells)
    {
        foreach (var cell in cells)
            _grid.Children.Add(cell);
    }

    private void RenderFigures(Figure?[,] figures)
    {
        foreach (var figure in figures)
            if (figure != null)
                _grid.Children.Add(figure);
    }

    private void Reset()
    {
        _grid.Children.Clear();
    }

    private void FillRowsAndColumns()
    {
        for (var i = 0; i < 8; i++)
        {
            _grid.ColumnDefinitions.Add(new ColumnDefinition());
            _grid.RowDefinitions.Add(new RowDefinition());
        }
    }

    private void BindCells(Cell[,] cells)
    {
        foreach (var cell in cells)
        {
            var source = new RelativeSource(RelativeSourceMode.Self);
            var bindColumn = new Binding("Column") { RelativeSource = source };
            var bindRow = new Binding("Row") { RelativeSource = source };
            cell.SetBinding(Grid.ColumnProperty, bindColumn);
            cell.SetBinding(Grid.RowProperty, bindRow);
        }
    }

    private void BindFigures(Figure?[,] figures)
    {
        foreach (var figure in figures)
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