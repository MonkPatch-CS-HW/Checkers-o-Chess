﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.Strategy;

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

        if (this.Any((move) => move.To == item.To))
            throw new Exception("Cannot add second move with the same destination");

        if (item.GetEatNumber() < 0)
            return;
        
        base.Add(item);
    }
    
    public void HighlightCells()
    {
        foreach (var move in this)
            move.HighlightCells();
    }

    public void ClearHighlighting()
    {
        foreach (var move in this)
            move.ClearHighlighting();
    }

    public Move? GetMoveByDestination(Cell cell)
    {
        foreach (var move in this)
            if (move.To == cell)
                return move;
        
        return null;
    }

    public bool CanEat()
    {
        return this.Any(move => move.GetEatNumber() > 0);
    }
}