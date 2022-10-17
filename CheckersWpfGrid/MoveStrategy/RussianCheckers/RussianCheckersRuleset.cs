using CheckersWpfGrid.MoveStrategy.RussianCheckers.Strategy;

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
    
    
}