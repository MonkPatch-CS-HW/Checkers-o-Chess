using System;
using System.Windows;
using System.Windows.Controls;

namespace CheckersWpfGrid;

public record Direction(int DirRow, int DirColumn)
{
    public int DirRow { get; } =
        DirRow is >= -1 and <= 1
            ? DirRow
            : throw new Exception($"Incorrect {nameof(DirRow)} value in {nameof(Direction)}");

    public int DirColumn { get; } =
        DirColumn is >= -1 and <= 1
            ? DirColumn
            : throw new Exception($"Incorrect {nameof(DirColumn)} value in {nameof(Direction)}");
}

public partial class Cell : UserControl
{
    public enum CellHighlightState
    {
        None,
        Available,
        Origin,
        Destination,
        Trace,
    }

    public enum CellKind
    {
        White,
        Black
    }

    public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
        nameof(Kind),
        typeof(CellKind),
        typeof(Cell),
        new PropertyMetadata());

    public static readonly DependencyProperty HighlightStateProperty = DependencyProperty.Register(
        nameof(HighlightState),
        typeof(CellHighlightState),
        typeof(Cell),
        new PropertyMetadata(CellHighlightState.None));

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

    public CellHighlightState HighlightState
    {
        get => (CellHighlightState)GetValue(HighlightStateProperty);
        set => SetValue(HighlightStateProperty, value);
    }

    public Cell? Relative(Direction direction)
    {
        var (distRow, distColumn) = direction;
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

    public Direction Direction(Cell other)
    {
        var distRow = other.Row - Row;
        var distColumn = other.Column - Column;
        if (Math.Abs(distRow) > Math.Abs(distColumn))
            return new Direction(Math.Sign(distRow), 0);
        if (Math.Abs(distRow) < Math.Abs(distColumn))
            return new Direction(0, Math.Sign(distColumn));
        return new Direction(Math.Sign(distRow), Math.Sign(distColumn));
    }
}