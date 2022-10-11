using CheckersWpfGrid.MoveStrategy.RussianCheckers.Base;

namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.Miss;

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