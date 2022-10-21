namespace CheckersWpfGrid.MoveStrategy.Corners.Strategy;

public class RegularStrategy : MoveStrategy
{
    public RegularStrategy(Ruleset ruleset) : base(ruleset)
    {
    }

    public override string Name => "Regular";
    
    public override Move? GetMove(Figure figure, Cell destination)
    {
        var straightDist = figure.Cell.StraightDist(destination);
        if (straightDist is > 2 or < 0)
            return null;
        if (destination.Figure != null)
            return null;
        if (straightDist == 1) return new Move(figure, destination);
        var nextCell = figure.Cell.Relative(figure.Cell.Direction(destination));
        return nextCell?.Figure == null ? null : new Move(figure, destination);
    }
}