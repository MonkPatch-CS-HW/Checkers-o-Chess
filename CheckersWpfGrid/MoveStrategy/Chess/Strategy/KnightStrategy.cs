using System;
using System.Collections.Generic;

namespace CheckersWpfGrid.MoveStrategy.Chess.Strategy;

public class KnightStrategy : MoveStrategy
{
    public KnightStrategy(Ruleset ruleset) : base(ruleset)
    {
    }

    public override string Name => "Knight";
    public override Move? GetMove(Figure figure, Cell destination)
    {
        var distRow = destination.Row - figure.Cell.Row;
        var distColumn = destination.Column - figure.Cell.Column;
        if (!(Math.Abs(distRow) == 1 && Math.Abs(distColumn) == 2) &&
            !(Math.Abs(distRow) == 2 && Math.Abs(distColumn) == 1))
            return null;
        if (destination.Figure != null && !figure.Player.IsEnemy(destination.Figure.Player))
            return null;
        var eatenFigures = new List<Figure>();
        if (destination.Figure != null) eatenFigures.Add(destination.Figure);
        return new Move(figure, destination)
        {
            EatenFigures = eatenFigures
        };
    }
}