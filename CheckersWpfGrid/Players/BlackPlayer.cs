using System;

namespace CheckersWpfGrid.Players;

public class BlackPlayer : Player
{
    public BlackPlayer(Game game) : base(game)
    {
    }

    public override string Name => "Black";

    public override PlayerKind Kind => PlayerKind.Black;

    public override bool CheckOppositeBorder(Cell cell)
    {
        return cell.Kind == Cell.CellKind.Black && cell.Row == 7;
    }
}