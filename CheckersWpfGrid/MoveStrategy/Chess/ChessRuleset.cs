using CheckersWpfGrid.MoveStrategy.Chess.GameState;
using CheckersWpfGrid.MoveStrategy.Chess.Strategy;

namespace CheckersWpfGrid.MoveStrategy.Chess;

public class ChessRuleset : Ruleset
{
    protected override MoveStrategy? CreateStrategy(string name)
    {
        return name switch
        {
            "Pawn" => new PawnStrategy(this),
            _ => null
        };
    }

    public override CheckersWpfGrid.MoveStrategy.GameState GetState(Game game)
    {
        var currentPlayer = GetCurrentPlayer(game);
        if (currentPlayer == null)
            return new WinnerGameState(CheckWinner(game));

        return new RegularGameState(currentPlayer);
    }

    public override Figure? GetStartFigure(Cell cell)
    {
        return cell.Row switch
        {
            1 => cell.Game.Players[0].AddFigure(cell, "Pawn"),
            6 => cell.Game.Players[1].AddFigure(cell, "Pawn"),
            _ => null
        };
    }

    public override int DeckSize => 8;
    public override string Name => "Chess";
}