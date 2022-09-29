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
        public MainWindow()
        {
            InitializeComponent();
            // TextBox.TextChanged += (a, b) =>
            // {
            //     Cell.Color = Brushes.Black;
            //     Figure.Sprite = (Brush)TryFindResource($"Figure{TextBox.Text}");
            //     // TextBox.Text = ;
            // };
            // return;

            for (var i = 0; i < 8; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.RowDefinitions.Add(new RowDefinition());
            }

            var cells = new Cell[8, 8];
            for (var r = 0; r < 8; r++)
            {
                for (var c = 0; c < 8; c++)
                {
                    var color = (c + r) % 2 == 0 ? Brushes.Black : Brushes.White;
                    var cell = new Cell { Color = color };
                    Grid.SetColumn(cell, c);
                    Grid.SetRow(cell, r);
                    Grid.Children.Add(cell);
                    cells[r, c] = cell;
                }
            }

            var sprite = (Brush)TryFindResource($"FigureWhite");
            var figure = new Figure { Cell = cells[0, 0], Sprite = sprite };
            figure.MouseDown += (a, b) =>
            {
                cells[1, 1].Figure = figure;
                figure.Cell = cells[1, 1];
            };
            Grid.Children.Add(figure);

            return;
            // for (var r = 0; r < 8; r++)
            // {
            //     for (var c = 0; c < 8; c++)
            //     {
            //         if ((c + r) % 2 != 0)
            //             continue;
            //         if (r is > 1 and < 6)
            //             continue;
            //         var name = c >= 6 ? "White" : "Black";
            //         var sprite = (Brush)TryFindResource($"Figure{name}");
            //         var figure = new Figure { Cell = cells[r, c], Sprite = sprite };
            //         Grid.SetRow(figure, r);
            //         Grid.SetColumn(figure, c);
            //         Grid.Children.Add(figure);
            //     }
            // }
        }
    }
}