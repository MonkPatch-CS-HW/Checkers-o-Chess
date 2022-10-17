using System.Collections.Generic;

namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.Strategy;

public abstract class BaseStrategy : MoveStrategy
{
    protected BaseStrategy(Ruleset ruleset) : base(ruleset)
    {
    }

    public override Move? GetMove(Figure figure, Cell destination)
    {
        if (figure.Cell.DiagonalDist(destination) < 0)
            return null;
        if (destination.Figure != null)
            return null;
        var eatenFigures = new List<Figure>();
        var currentCell = figure.Cell;
        while (currentCell != destination)
        {
            var prevCell = currentCell;
            var nextCell = currentCell.Relative(currentCell.Direction(destination));
            if (nextCell == null)
                return null;
            currentCell = nextCell;
            if (prevCell.Figure != null && currentCell.Figure != null)
                continue;
            if (prevCell.Figure != null && prevCell.Figure.Player.IsEnemy(figure.Player))
                eatenFigures.Add(prevCell.Figure);
        }

        return new Move(figure, destination)
        {
            EatenFigures = eatenFigures
        };
    }
}