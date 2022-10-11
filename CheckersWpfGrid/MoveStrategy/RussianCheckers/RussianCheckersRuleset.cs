using CheckersWpfGrid.MoveStrategy.RussianCheckers.Miss;
using CheckersWpfGrid.MoveStrategy.RussianCheckers.Regular;

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
}