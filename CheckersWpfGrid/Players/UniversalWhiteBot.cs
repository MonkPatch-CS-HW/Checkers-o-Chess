using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid;

public class UniversalWhiteBot : Player
{
    public UniversalWhiteBot(Game game) : base(game)
    {
        Game.AfterMove += GameAfterMove;
    }

    private async void GameAfterMove(Move _)
    {
        if (Game.CurrentPlayer != this)
            return;
        await Task.Delay(500);
        MakeMove();
    }

    private void MakeMove()
    {
        var moves = GetAvailableFigures().SelectMany(figure => figure.Strategy.GetMoves(figure)).ToList();
        var eatingMoves = moves.Where(move => move.Eats).ToList();
        var move = eatingMoves.Count > 0 ? eatingMoves[0] : moves[0];

        Game.CommitMove(move);
    }

    public override string Name => "WhiteBot";

    public override PlayerKind Kind => PlayerKind.White;
    public override bool IsBot => true;

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