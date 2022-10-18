using System.Collections.Generic;
using System.Windows.Documents;

namespace CheckersWpfGrid.MoveStrategy.Chess.Strategy;

public class QueenStrategy : MoveStrategy
{
    public QueenStrategy(Ruleset ruleset) : base(ruleset)
    {
    }

    public override string Name => "Queen";
    public override Move? GetMove(Figure figure, Cell destination)
    {
        if (figure.Cell.DiagonalDist(destination) < 0 && figure.Cell.StraightDist(destination) < 0)
            return null;
        var currentCell = figure.Cell;
        while (currentCell != destination)
        {
            var nextCell = currentCell.Relative(currentCell.Direction(destination));
            if (nextCell == null || (nextCell != destination && nextCell.Figure != null))
                return null;
            currentCell = nextCell;
        }
        
        if (destination.Figure != null && !figure.Player.IsEnemy(destination.Figure.Player))
            return null;
        
        var eatenFigures = new List<Figure>();
        if (destination.Figure != null) eatenFigures.Add(destination.Figure);
        return new Move(figure, destination) { EatenFigures = eatenFigures };
    }
}