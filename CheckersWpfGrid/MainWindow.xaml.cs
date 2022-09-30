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

namespace CheckersWpfGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cell[,] _cells;
        private Figure?[,] _figures;

        public MainWindow()
        {
            InitializeComponent();
            FillRowsAndColumns();
            _cells = GenerateCells();
            _figures = GenerateFigures();
            BindCells(_cells);
            BindFigures(_figures);
            RenderCells(_cells);
            RenderFigures(_figures);
        }

        private void RenderCells(Cell[,] cells)
        {
            foreach (var cell in cells)
                Grid.Children.Add(cell);
        }

        private void RenderFigures(Figure?[,] figures)
        {
            foreach (var figure in figures)
                if (figure != null)
                    Grid.Children.Add(figure);
        }

        private void FillRowsAndColumns()
        {
            Grid.Children.Clear();
            for (var i = 0; i < 8; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private Cell[,] GenerateCells()
        {
            var cells = new Cell[8, 8];
            for (var r = 0; r < 8; r++)
            {
                for (var c = 0; c < 8; c++)
                {
                    var color = (c + r) % 2 == 0 ? Brushes.Black : Brushes.White;
                    var cell = new Cell { Color = color, Column = c, Row = r };
                    cells[r, c] = cell;
                }
            }

            return cells;
        }

        private Figure?[,] GenerateFigures()
        {
            var figures = new Figure?[8, 8];
            for (var r = 0; r < 8; r++)
            {
                for (var c = 0; c < 8; c++)
                {
                    if ((c + r) % 2 != 0)
                        continue;
                    if (r is > 2 and < 5)
                        continue;
                    var name = r > 3 ? "White" : "Black";
                    var sprite = (Brush)TryFindResource($"Figure{name}");
                    var figure = new Figure { Sprite = sprite, Column = c, Row = r };
                    figures[r, c] = figure;
                }
            }

            return figures;
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
}