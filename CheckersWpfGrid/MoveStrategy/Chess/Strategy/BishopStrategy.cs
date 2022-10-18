using System.Collections.Generic;

namespace CheckersWpfGrid.MoveStrategy.Chess.Strategy;

public class BishopStrategy : MoveStrategy
{
    public BishopStrategy(Ruleset ruleset) : base(ruleset)
    {
    }

    public override string Name => "Bishop";

    public override Move? GetMove(Figure figure, Cell destination)
    {
        if (figure.Cell.DiagonalDist(destination) < 0)
            return null;
        if (destination.Figure != null && !figure.Player.IsEnemy(destination.Figure.Player))
            return null;
        
        var eatenFigures = new List<Figure>();
        if (destination.Figure != null)
            eatenFigures.Add(destination.Figure);

        var currentCell = figure.Cell;
        while (currentCell != destination)
        {
            var nextCell = currentCell.Relative(currentCell.Direction(destination));
            if (nextCell == null || (nextCell != destination && nextCell.Figure != null))
                return null;
            currentCell = nextCell;
        }

        return new Move(figure, destination) { EatenFigures = eatenFigures };
    }
}