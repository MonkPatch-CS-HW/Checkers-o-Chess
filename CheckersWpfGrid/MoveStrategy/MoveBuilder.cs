using System;
using System.Collections.Generic;
using System.Windows;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid.MoveStrategy;

abstract public class MoveBuilder
{
    protected Figure Figure { get; }
    protected Cell StartCell { get; }
    protected List<Cell> Cells { get; } = new List<Cell>();
    protected List<Figure> EatenFigures { get; } = new List<Figure>();

    protected MoveStrategy? DestinationStrategy { get; private set; } = null;
    protected int GameHash { get; }

    protected MoveBuilder(Figure figure)
    {
        Figure = figure;
        StartCell = figure.Cell;
        DestinationStrategy = figure.Strategy;
        GameHash = figure.Game.GetHashCode();
    }

    protected Cell LastCell => Cells.Count > 0 ? Cells[^1] : StartCell;

    protected bool EatsFigure(Figure figure)
    {
        return Figure.Player != figure.Player;
    }

    public bool CheckNext(Direction direction) => true;

    public bool Next(Direction direction)
    {
        if (!CheckNext(direction))
            return false;
        var nextCell = LastCell.Relative(direction);
        if (nextCell == null)
            throw new Exception("Moved to direction of another cell and got null cell...");
        if (nextCell.Figure != null && EatsFigure(nextCell.Figure))
            EatenFigures.Add(nextCell.Figure);
        Cells.Add(nextCell);
        return true;
    }
    
    public abstract bool CheckDestination(Cell cell);

    public MoveBuilder To(Cell cell)
    {
        if (!CheckDestination(cell))
            throw new Exception($"Cannot move to {cell}");
        
        while (LastCell != cell)
        {
            if (!Next(LastCell.Direction(cell)))
                throw new Exception($"Could not move figure to {cell}");
        }

        return this;
    }

    public bool CheckAll()
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            if (Cells[i] == LastCell && Cells[i - 1].Figure != null)
                return false;
            if (Cells[i] == LastCell && Cells[i].Figure != null)
                return false;
            if (i > 0 && Cells[i].Figure != null && Cells[i - 1].Figure != null)
                return false;
            if (Cells[i].Figure != null && Cells[i].Figure!.Player == Figure.Player)
                return false;
        }

        return true;
    }

    public MoveBuilder Stratregy(MoveStrategy strategy)
    {
        DestinationStrategy = strategy;
        return this;
    }

    public Move Build()
    {
        if (Figure.Game.GetHashCode() != GameHash)
            throw new Exception("Game has changed between creation of move builder and building");
        if (!CheckAll())
            throw new Exception("Trying to build incorrect move");
        
        var move = new Move(Figure, LastCell)
        {
            EatenFigures = EatenFigures,
            DestinationStrategy = DestinationStrategy ?? Figure.Strategy,
        };
        return move;
    }
}