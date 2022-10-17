namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.Strategy;

public class MissStrategy : BaseStrategy
{
    public MissStrategy(Ruleset ruleset) : base(ruleset)
    {
    }


    public override string Name => "Miss";
}