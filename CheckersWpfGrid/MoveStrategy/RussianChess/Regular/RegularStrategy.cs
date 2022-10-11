using System.Windows;

namespace CheckersWpfGrid.MoveStrategy.RussianChess.Regular;

public class RegularStrategy : MoveStrategy
{
    public RegularStrategy(Ruleset ruleset) : base(ruleset)
    {
    }


    public override string Name => "Regular";

    public override Move? GetMove(Figure figure, Cell cell)
    {
        var builder = new RegularMoveBuilder(figure);
        if (!builder.CheckDestination(cell))
            return null;
        if (figure.Player.CheckOppositeBorder(cell))
        {
            builder.AfterExecute(OnExecute).BeforeUndo(OnUndo);
        }
        builder.To(cell);
        if (!builder.CheckAll())
            return null;
        return builder.Build();
    }

    protected void OnExecute(Figure figure)
    {
        figure.Strategy = Ruleset.GetStrategy("Miss");
    }

    protected void OnUndo(Figure figure)
    {
        figure.Strategy = Ruleset.GetStrategy("Original");
    }
}