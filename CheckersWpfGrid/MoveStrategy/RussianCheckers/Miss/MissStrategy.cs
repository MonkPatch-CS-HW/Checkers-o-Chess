namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.Miss;

public class MissStrategy : MoveStrategy
{
    public MissStrategy(Ruleset ruleset) : base(ruleset)
    {
    }


    public override string Name => "Miss";

    public override Move? GetMove(Figure figure, Cell cell)
    {
        var builder = new MissMoveBuilder(figure);
        if (!builder.CheckDestination(cell))
            return null;
        builder.To(cell);
        if (!builder.CheckAll())
            return null;
        return builder.Build();
    }
}