using CheckersWpfGrid.MoveStrategy.RussianChess.Base;

namespace CheckersWpfGrid.MoveStrategy.RussianChess.Miss;

public class MissMoveBuilder : BaseMoveBuilder
{
    public MissMoveBuilder(Figure figure) : base(figure)
    {
    }

    public override bool CheckDestination(Cell cell)
    {
        return Figure.Cell.DiagonalDist(cell) is > 0;
    }
}