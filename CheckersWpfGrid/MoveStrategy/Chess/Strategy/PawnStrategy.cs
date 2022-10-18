using System.Collections.Generic;

namespace CheckersWpfGrid.MoveStrategy.Chess.Strategy;

public class PawnStrategy : MoveStrategy
{
    public override string Name => "Pawn";

    public override Move? GetMove(Figure figure, Cell destination)
    {
        var dirRow = figure.Cell.Direction(destination).DirRow;
        if (figure.Player == figure.Game.Players[0] && dirRow != 1)
            return null;
        if (figure.Player == figure.Game.Players[1] && dirRow != -1)
            return null;
        if (destination.Figure != null && figure.Player.IsEnemy(destination.Figure.Player) &&
            figure.Cell.DiagonalDist(destination) == 1)
            return new Move(figure, destination) { EatenFigures = new List<Figure> { destination.Figure } };

        if (destination.Figure == null && figure.Cell.Relative(figure.Cell.Direction(destination))?.Figure == null &&
            figure.Cell.StraightDist(destination) == 2)
        {
            if ((figure.Player == figure.Game.Players[0] && figure.Row == 1) ||
                (figure.Player == figure.Game.Players[1] && figure.Row == 6))
                return new Move(figure, destination);
        }

        if (destination.Figure == null && figure.Cell.StraightDist(destination) == 1)
            return new Move(figure, destination);

        return null;
    }

    public PawnStrategy(Ruleset ruleset) : base(ruleset)
    {
    }
}