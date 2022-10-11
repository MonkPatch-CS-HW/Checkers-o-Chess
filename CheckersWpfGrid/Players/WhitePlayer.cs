namespace CheckersWpfGrid;

public class WhitePlayer : Player
{
    public WhitePlayer(Game game) : base(game)
    {
    }

    public override string Name => "White";

    public override PlayerKind Kind => PlayerKind.White;

    protected override Figure? CreateFigure(Cell cell)
    {
        if (cell.Kind != Cell.CellKind.Black || cell.Row <= 4)
            return null;
        return new Figure(Game, this)
        {
            Row = cell.Row,
            Column = cell.Column
        };
    }

    public override bool CheckOppositeBorder(Cell cell)
    {
        return cell.Kind == Cell.CellKind.Black && cell.Row == 0;
    }
}