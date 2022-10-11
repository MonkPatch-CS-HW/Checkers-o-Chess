namespace CheckersWpfGrid;

public class BlackPlayer : Player
{
    public BlackPlayer(Game game) : base(game)
    {
    }

    public override string Name => "Black";

    public override PlayerKind Kind => PlayerKind.Black;

    public override Figure? GetStartFigure(Cell cell)
    {
        if (cell.Kind != Cell.CellKind.Black || cell.Row >= 3)
            return null;
        return new Figure(Game, this)
        {
            Row = cell.Row,
            Column = cell.Column
        };
    }

    public override bool CheckOppositeBorder(Cell cell)
    {
        return cell.Kind == Cell.CellKind.Black && cell.Row == 7;
    }
}