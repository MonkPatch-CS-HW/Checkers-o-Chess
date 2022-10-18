using System.Collections.Generic;
using System.Windows.Documents;

namespace CheckersWpfGrid.MoveStrategy.Chess.Strategy;

public class KingStrategy : MoveStrategy
{
    public KingStrategy(Ruleset ruleset) : base(ruleset)
    {
    }

    public override string Name => "King";
    public override Move? GetMove(Figure figure, Cell destination)
    {
        if (figure.Cell.DiagonalDist(destination) < 0 && figure.Cell.StraightDist(destination) < 0)
            return null;
        if (figure.Cell.DiagonalDist(destination) > 1 || figure.Cell.StraightDist(destination) > 1)
            return null;
        if (destination.Figure != null && !figure.Player.IsEnemy(destination.Figure.Player))
            return null;
        var eatenFigures = new List<Figure>();
        if (destination.Figure != null) eatenFigures.Add(destination.Figure);
        return new Move(figure, destination) { EatenFigures = eatenFigures };
    }
}