using System;
using System.Collections.Generic;

namespace CheckersWpfGrid.MoveStrategy;

public class Move
{
    public Move(Figure figure, List<Cell> path, Cell destination)
    {
        Figure = figure;
        OriginHash = figure.Game.GetHashCode();
        Origin = figure.Cell;
        Destination = destination;
        Path = path;
    }

    public Figure Figure { get; }

    public Cell Origin { get; init; }
    public Cell Destination { get; }
    public List<Figure> EatenFigures { get; init; } = new();
    public List<Cell> Path { get; }

    public int OriginHash { get; }
    public int DestinationHash { get; private set; }

    public bool Eats => EatenFigures.Count > 0;

    public event Action<Figure>? OnExecute;
    public event Action<Figure>? OnUndo;

    public void Execute()
    {
        if (Figure.Game.GetHashCode() != OriginHash)
            throw new Exception("Cannot make move in changed game");

        Figure.Cell = Destination;
        foreach (var figure in EatenFigures)
            figure.Active = false;

        OnExecute?.Invoke(Figure);
        DestinationHash = Figure.Game.GetHashCode();
    }

    public void Undo()
    {
        if (Figure.Game.GetHashCode() != DestinationHash)
            throw new Exception("Cannot undo move in incorrect game state");

        OnUndo?.Invoke(Figure);
        foreach (var figure in EatenFigures)
            figure.Active = true;

        Figure.Cell = Origin;
    }
}