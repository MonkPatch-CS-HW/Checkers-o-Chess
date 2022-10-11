using System.Collections.Generic;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid;

public class Highlighter
{
    protected List<Cell> Highlighted { get; } = new List<Cell>();
    protected Game Game { get; }
    public Highlighter(Game game)
    {
        Game = game;
    }

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

    public Highlighter HighlightFigures(List<Figure> figures)
    {
        foreach (var figure in figures)
        {
            figure.Cell.HighlightState = Cell.CellHighlightState.Available;
            Highlighted.Add(figure.Cell);
        }

        return this;
    }

    public Highlighter HighlightMoveSet(MoveSet moveSet)
    {
        var origin = new List<Cell>();
        var path = new List<Cell>();
        var destination = new List<Cell>();
        foreach (var move in moveSet)
        {
            origin.Add(move.Origin);
            foreach (var cell in move.Path)
            {
                path.Add(cell);
            }
            destination.Add(move.Destination);
        }

        foreach (var cell in origin)
        {
            cell.HighlightState = Cell.CellHighlightState.Origin;
            Highlighted.Add(cell);
        }
        foreach (var cell in path)
        {
            cell.HighlightState = Cell.CellHighlightState.Path;
            Highlighted.Add(cell);
        }
        foreach (var cell in destination)
        {
            cell.HighlightState = Cell.CellHighlightState.Destination;
            Highlighted.Add(cell);
        }
        return this;
    }
}