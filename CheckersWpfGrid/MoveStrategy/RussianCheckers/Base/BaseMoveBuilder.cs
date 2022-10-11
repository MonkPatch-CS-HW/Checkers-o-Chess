using System;

namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.Base;

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
        for (var i = 0; i < Path.Count; i++)
        {
            if (Path[i] == LastCell && Path[i].Figure != null)
                return false;
            if (i > 0 && Path[i].Figure != null && Path[i - 1].Figure != null)
                return false;
            if (Path[i].Figure != null && Path[i].Figure!.Player == Figure.Player)
                return false;
        }

        return true;
    }
}