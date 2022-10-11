using System;
using System.Collections.Generic;
using System.Security.Policy;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid.MoveStrategy;

public class Move
{
    public Figure Figure { get; }

    public Cell Origin { get; init; }
    public Cell Destination { get; }
    public List<Figure> EatenFigures { get; init; } = new List<Figure>();
    public MoveStrategy OriginStrategy { get; init; }
    public MoveStrategy DestinationStrategy { get; init; }

    public int OriginHash { get; }
    public int DestinationHash { get; private set; }

    public Move(Figure figure, Cell destination)
    {
        Figure = figure;
        OriginHash = figure.Game.GetHashCode();
        Origin = figure.Cell;
        Destination = destination;
        OriginStrategy = figure.Strategy;
        DestinationStrategy = OriginStrategy;
    }

    public bool Eats => EatenFigures.Count > 0;

    public void Execute()
    {
        if (Figure.Game.GetHashCode() != OriginHash)
            throw new Exception("Cannot make move in changed game");
        
        Figure.Cell = Destination;
        foreach (var figure in EatenFigures)
            figure.Active = false;

        Figure.Strategy = DestinationStrategy;
        DestinationHash = Figure.Game.GetHashCode();
    }

    public void Undo()
    {
        if (Figure.Game.GetHashCode() != DestinationHash)
            throw new Exception("Cannot undo move in incorrect game state");
        
        Figure.Strategy = OriginStrategy;
        foreach (var figure in EatenFigures)
            figure.Active = true;

        Figure.Cell = Origin;
    }

    public void HighlightCells()
    {
        Destination.Highlighted = true;
    }

    public void ClearHighlighting()
    {
        Destination.Highlighted = false;
    }
}