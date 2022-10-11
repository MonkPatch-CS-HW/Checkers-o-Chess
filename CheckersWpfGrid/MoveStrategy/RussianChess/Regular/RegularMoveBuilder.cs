using CheckersWpfGrid.MoveStrategy.RussianChess.Base;

namespace CheckersWpfGrid.MoveStrategy.RussianChess.Regular;

public class RegularMoveBuilder : BaseMoveBuilder
{
    public RegularMoveBuilder(Figure figure) : base(figure)
    {
    }

    public override bool CheckDestination(Cell cell)
    {
        return Figure.Cell.DiagonalDist(cell) is >= 1 and <= 2;
    }

    public new bool CheckAll()
    {
        if (Path.Count == 2 && Path[0].Figure == null)
            return false;
        return base.CheckAll();
    }
}