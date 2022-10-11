namespace CheckersWpfGrid.MoveStrategy.RussianChess.Miss;

public class MissMoveBuilder : MoveBuilder
{
    public MissMoveBuilder(Figure figure) : base(figure)
    {
    }

    public override bool CheckDestination(Cell cell)
    {
        return Figure.Cell.DiagonalDist(cell) is > 0;
    }
}