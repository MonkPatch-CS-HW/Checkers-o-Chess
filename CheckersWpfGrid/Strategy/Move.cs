using System;
using System.Collections.Generic;

namespace CheckersWpfGrid.Strategy;

public abstract class Move
{
    public enum MoveState
    {
        Idle,
        Executed,
    }

    public readonly Figure Figure;
    public readonly Cell From;
    public readonly List<Cell> Through;
    public readonly Cell To;
    public readonly List<Figure> Deleted = new List<Figure>();
    public readonly MoveStrategy FromStrategy;
    public readonly MoveStrategy ToStrategy;

    public Move(Figure figure, List<Cell> through, Cell to, MoveStrategy? toStrategy)
    {
        Figure = figure;
        From = figure.Cell;
        Through = through;
        To = to;
        FromStrategy = figure.Strategy;
        ToStrategy = toStrategy ?? figure.Strategy;
    }

    public MoveState State { get; private set; }

    // To cache value

    public abstract int GetEatNumber();

    public void Execute()
    {
        if (GetEatNumber() < 0)
            throw new Exception("Cannot play incorrect move");
        
        if (State != MoveState.Idle)
            throw new Exception("Cannot make move in incorrect state");

        Figure.Cell = To;
        foreach (var cell in Through)
        {
            if (cell.Figure == null || cell.Figure.Active == false || cell.Figure.Player == Figure.Player)
                continue;
            cell.Figure!.Active = false;
            Deleted.Add(cell.Figure!);
        }

        Figure.Strategy = ToStrategy;
        State = MoveState.Executed;
    }

    public void Undo()
    {
        if (State != MoveState.Executed)
            throw new Exception("Cannot undo move in incorrect state");

        Figure.Strategy = FromStrategy;
        foreach (var figure in Deleted)
        {
            figure.Active = true;
        }

        Figure.Cell = From;
        State = MoveState.Idle;
    }

    public void HighlightCells()
    {
        foreach (var cell in Through)
            if (cell.PathState != Cell.CellPathState.To)
                cell.PathState = Cell.CellPathState.Through;
        To.PathState = Cell.CellPathState.To;
    }

    public void ClearHighlighting()
    {
        foreach (var cell in Through)
            cell.PathState = Cell.CellPathState.None;
        To.PathState = Cell.CellPathState.None;
    }
}