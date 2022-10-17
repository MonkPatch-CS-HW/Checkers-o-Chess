using System.Collections.Generic;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid;

public class Highlighter
{
    public Highlighter()
    {
    }

    protected List<Cell> Highlighted { get; } = new();

    public Highlighter ClearHighlighting()
    {
        while (Highlighted.Count > 0)
        {
            var cell = Highlighted[0];
            Highlighted.RemoveAt(0);
            cell.HighlightState = Cell.CellHighlightState.None;
        }

        return this;
    }

    public Highlighter HighlightFigures(List<Figure>? figures)
    {
        if (figures == null)
            return this;
        
        foreach (var figure in figures)
        {
            figure.Cell.HighlightState = Cell.CellHighlightState.Available;
            Highlighted.Add(figure.Cell);
        }

        return this;
    }

    public Highlighter HighlightMoves(MoveSet? moveSet)
    {
        if (moveSet == null)
            return this;
        
        var origin = new List<Cell>();
        var destination = new List<Cell>();
        foreach (var move in moveSet)
        {
            origin.Add(move.Origin);
            destination.Add(move.Destination);
        }

        foreach (var cell in origin)
        {
            cell.HighlightState = Cell.CellHighlightState.Origin;
            Highlighted.Add(cell);
        }

        foreach (var cell in destination)
        {
            cell.HighlightState = Cell.CellHighlightState.Destination;
            Highlighted.Add(cell);
        }

        return this;
    }

    public Highlighter HighlightTrace(Move? move)
    {
        if (move == null)
            return this;
        
        move.Origin.HighlightState = Cell.CellHighlightState.Trace;
        Highlighted.Add(move.Origin);

        move.Destination.HighlightState = Cell.CellHighlightState.Trace;
        Highlighted.Add(move.Destination);
        
        return this;
    }
}