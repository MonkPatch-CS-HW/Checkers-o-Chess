using System;

namespace CheckersWpfGrid.MoveStrategy.RussianChess.Base;

public abstract class BaseMoveBuilder : MoveBuilder
{
    protected BaseMoveBuilder(Figure figure) : base(figure)
    {
    }

    public override bool CheckNext(Direction direction)
    {
        return Math.Abs(direction.DirRow) + Math.Abs(direction.DirColumn) == 2;
    }

    public override bool CheckAll()
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
}