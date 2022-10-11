using CheckersWpfGrid.MoveStrategy;
using CheckersWpfGrid.MoveStrategy.RussianChess.Miss;
using CheckersWpfGrid.MoveStrategy.RussianChess.Regular;

namespace CheckersWpfGrid.MoveStrategy.RussianChess;

public class RussianChessRuleset : Ruleset
{
    protected override MoveStrategy? CreateStrategy(string name)
    {
        return name switch
        {
            "Regular" => new RegularStrategy(this),
            "Miss" => new MissStrategy(this),
            _ => null
        };
        return null;
    }
}