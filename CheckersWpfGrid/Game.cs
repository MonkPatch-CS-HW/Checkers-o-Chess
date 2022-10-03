using System.Windows;
using System.Windows.Media;

namespace CheckersWpfGrid;

public class Game
{
    public readonly Cell[,] Cells = new Cell[8, 8];
    public readonly Figure?[,] Figures = new Figure[8, 8];
    public readonly Player[] Players = new Player[2];

    public Game()
    {
        Cells = CreateCells();
        Players = CreatePlayers();
        Figures = CreateFigures();
    }

    private Cell[,] CreateCells()
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

    private Player[] CreatePlayers()
    {
        var first = new BlackPlayer();
        var second = new WhitePlayer();
        return new Player[] { first, second };
    }

    private Figure?[,] CreateFigures()
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
                foreach (var player in Players)
                {
                    if (!player.CheckStartPosition(r, c))
                        continue;

                    var figure = new Figure { Column = c, Row = r, Player = player };
                    figures[r, c] = figure;
                    figure.MouseDown += (sender, args) =>
                    {
                        figure.Player = null;
                    };
                    break;
                }
            }
        }

        return figures;
    }
}