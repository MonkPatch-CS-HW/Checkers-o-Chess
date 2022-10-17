namespace CheckersWpfGrid.Players;

public class WhitePlayer : Player
{
    public WhitePlayer(Game game) : base(game)
    {
    }

    public override string Name => "White";

    public override PlayerKind Kind => PlayerKind.White;

    public override bool CheckOppositeBorder(Cell cell)
    {
        return cell.Kind == Cell.CellKind.Black && cell.Row == 0;
    }
}