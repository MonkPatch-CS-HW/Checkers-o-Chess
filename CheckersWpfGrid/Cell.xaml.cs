using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckersWpfGrid;

public partial class Cell : UserControl
{
    public enum CellKind
    {
        White,
        Black,
    };

    public enum CellPathState
    {
        None,
        Through,
        To,
    }

    public readonly Game Game;

    public Cell(Game game)
    {
        InitializeComponent();
        Game = game;
    }

    public int Row { get; init; }
    public int Column { get; init; }

    public Figure? Figure
    {
        get => Game.Board[Row, Column];
        set
        {
            if (value == null)
                return;

            Game.Board[Row, Column] = value;
        }
    }

    public CellKind Kind
    {
        get => (CellKind)GetValue(KindProperty);
        init => SetValue(KindProperty, value);
    }

    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind),
        typeof(CellKind),
        typeof(Cell),
        new PropertyMetadata());

    public Cell? Relative(int distRow, int distColumn)
    {
        var (row, column) = (Row + distRow, Column + distColumn);
        if (!Game.Table.Contains(row, column))
            return null;
        return Game.Table[row, column];
    }

    public int DiagonalDist(Cell other)
    {
        var distRow = Math.Abs(other.Row - Row);
        var distColumn = Math.Abs(other.Column - Column);
        if (distRow == distColumn)
            return distRow;
        return -1;
    }

    public CellPathState PathState
    {
        get => (CellPathState)GetValue(PathStateProperty);
        set => SetValue(PathStateProperty, value);
    }

    public static readonly DependencyProperty PathStateProperty = DependencyProperty.Register(
        nameof(PathState),
        typeof(CellPathState),
        typeof(Cell),
        new PropertyMetadata(CellPathState.None));
}