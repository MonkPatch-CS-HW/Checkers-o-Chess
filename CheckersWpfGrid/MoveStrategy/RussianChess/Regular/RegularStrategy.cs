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
            builder.Stratregy(Ruleset.GetStrategy("Miss"));
        builder.To(cell);
        if (!builder.CheckAll())
            return null;
        return builder.Build();
    }
}