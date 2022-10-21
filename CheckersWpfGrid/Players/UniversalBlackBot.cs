using System;
using System.Linq;
using System.Threading.Tasks;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid.Players;

public class UniversalBlackBot : Player
{
    public UniversalBlackBot(Game game) : base(game)
    {
        Game.AfterStateUpdate += OnStateUpdate;
    }

    private async void OnStateUpdate(GameState _)
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
        var resultMoves = eatingMoves.Count > 0 ? eatingMoves : moves;
        if (resultMoves.Count == 0)
        {
            Surrender();
            return;
        }
        var rand = new Random((int)DateTimeOffset.Now.ToUnixTimeSeconds()).Next();
        var move = resultMoves[rand % resultMoves.Count];
        Game.CommitMove(move);
    }

    public override string Name => "BlackBot";

    public override PlayerKind Kind => PlayerKind.Black;
    public override bool IsBot => true;

    public override bool CheckOppositeBorder(Cell cell)
    {
        return cell.Kind == Cell.CellKind.Black && cell.Row == 0;
    }
}