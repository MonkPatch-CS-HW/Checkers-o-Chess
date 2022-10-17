using CheckersWpfGrid.MoveStrategy.RussianCheckers.Strategy;
using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy.RussianCheckers;

public class RussianCheckersRuleset : Ruleset
{
    public override int DeckSize => 8;
    public override string Name => "Russian checkers";

    protected override MoveStrategy? CreateStrategy(string name)
    {
        return name switch
        {
            "Regular" => new RegularStrategy(this),
            "Miss" => new MissStrategy(this),
            _ => null
        };
    }

    protected override Player? GetCurrentPlayer(Game game)
    {
        var nextPlayer = base.GetCurrentPlayer(game);
        if (nextPlayer == null)
            return null;
        if (game.LastMove != null && game.LastMove.EatenFigures.Count > 0 && game.LastMove.Figure.CanEat())
            return game.LastMove.Figure.Player;
        return nextPlayer;
    }

    public override bool CanSelectFigure(Player player, Figure figure)
    {
        if (player.Game.LastMove?.Figure.Player == player && player.Game.LastMove is { Eats: true } && player.Game.LastMove.Figure != figure)
            return false;
        return base.CanSelectFigure(player, figure);
    }

    public override Figure? GetStartFigure(Cell cell)
    {
        if ((cell.Row + cell.Column) % 2 == 0)
            return null;

        return cell.Row switch
        {
            <= 2 => cell.Game.Players[0].AddFigure(cell, "Regular"),
            >= 5 => cell.Game.Players[1].AddFigure(cell, "Regular"),
            _ => null
        };
    }
}