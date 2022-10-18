using System.Linq;
using CheckersWpfGrid.MoveStrategy.Chess.GameState;
using CheckersWpfGrid.MoveStrategy.Chess.Strategy;
using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy.Chess;

public class ChessRuleset : Ruleset
{
    protected override MoveStrategy? CreateStrategy(string name)
    {
        return name switch
        {
            "Pawn" => new PawnStrategy(this),
            "Rook" => new RookStrategy(this),
            "Bishop" => new BishopStrategy(this),
            "Knight" => new KnightStrategy(this),
            "King" => new KingStrategy(this),
            "Queen" => new QueenStrategy(this),
            _ => null
        };
    }

    public override CheckersWpfGrid.MoveStrategy.GameState GetState(Game game)
    {
        var currentPlayer = GetCurrentPlayer(game);
        if (currentPlayer == null)
            return new WinnerGameState(CheckWinner(game));
        var check = CheckCheck(game);
        if (check != null)
            return new CheckGameState(currentPlayer, check);

        return new RegularGameState(currentPlayer);
    }

    private Player? CheckCheck(Game game)
    {
        var figures = game.Board.Figures;
        foreach (var figure in figures)
        {
            if (figure == null)
                continue;

            var move = figure.Strategy.GetMoves(figure)
                .Find(move => move.EatenFigures.Any(eaten => eaten.Strategy.Name == "King"));
            if (move == null) continue;
            
            var eatenFigure = move.EatenFigures.Find(eaten => eaten.Strategy.Name == "King");
            return eatenFigure?.Player;
        }

        return null;
    }

    protected override Player? CheckWinner(Game game)
    {
        Player? winner = null;
        bool hasWinner = false;
        
        foreach (var player in game.Players)
        {
            var king = player.Figures.Find(figure => figure.Strategy.Name == "King");
            if (king != null)
                winner = player;
            if (king == null)
                hasWinner = true;
        }

        return hasWinner ? winner : base.CheckWinner(game);
    }

    public override Figure? GetStartFigure(Cell cell)
    {
        return (cell.Row, cell.Column) switch
        {
            (0, 0 or 7) => cell.Game.Players[0].AddFigure(cell, "Rook"),
            (7, 0 or 7) => cell.Game.Players[1].AddFigure(cell, "Rook"),
            (0, 2 or 5) => cell.Game.Players[0].AddFigure(cell, "Bishop"),
            (7, 2 or 5) => cell.Game.Players[1].AddFigure(cell, "Bishop"),
            (0, 1 or 6) => cell.Game.Players[0].AddFigure(cell, "Knight"),
            (7, 1 or 6) => cell.Game.Players[1].AddFigure(cell, "Knight"),
            (0, 4) => cell.Game.Players[0].AddFigure(cell, "King"),
            (7, 4) => cell.Game.Players[1].AddFigure(cell, "King"),
            (0, 3) => cell.Game.Players[0].AddFigure(cell, "Queen"),
            (7, 3) => cell.Game.Players[1].AddFigure(cell, "Queen"),
            (1, _) => cell.Game.Players[0].AddFigure(cell, "Pawn"),
            (6, _) => cell.Game.Players[1].AddFigure(cell, "Pawn"),
            _ => null
        };
    }

    public override int DeckSize => 8;
    public override string Name => "Chess";
}