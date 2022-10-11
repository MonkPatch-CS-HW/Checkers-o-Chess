using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.MoveStrategy;

public class MoveSet : List<Move>
{
    public readonly Figure Figure;

    public MoveSet(Figure figure)
    {
        Figure = figure;
    }

    public new void Add(Move? item)
    {
        if (item == null)
            return;

        if (item.Figure != Figure)
            throw new Exception("Cannot add other figure's move");

        if (this.Any(move => move.Destination == item.Destination))
            throw new Exception("Cannot add second move with the same destination");

        base.Add(item);
    }

    public Move? GetMoveByDestination(Cell cell)
    {
        foreach (var move in this)
            if (move.Destination == cell)
                return move;

        return null;
    }

    public bool CanEat()
    {
        return this.Any(move => move.Eats);
    }

    public bool CanMove()
    {
        return Count > 0;
    }
}