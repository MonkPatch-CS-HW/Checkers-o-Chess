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

    protected Action<Figure>? ExecuteHandler;
    protected Action<Figure>? UndoHandler;
    protected int GameHash { get; }

    protected MoveBuilder(Figure figure)
    {
        Figure = figure;
        StartCell = figure.Cell;
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

    public MoveBuilder AfterExecute(Action<Figure> executeHandler)
    {
        ExecuteHandler = executeHandler;
        return this;
    }

    public MoveBuilder BeforeUndo(Action<Figure> undoHandler)
    {
        UndoHandler = undoHandler;
        return this;
    }

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
            if (Cells[i] == LastCell && Cells[i].Figure != null)
                return false;
            if (i > 0 && Cells[i].Figure != null && Cells[i - 1].Figure != null)
                return false;
            if (Cells[i].Figure != null && Cells[i].Figure!.Player == Figure.Player)
                return false;
        }

        return true;
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
        };
        move.OnExecute += ExecuteHandler;
        move.OnUndo += UndoHandler;
        return move;
    }
}