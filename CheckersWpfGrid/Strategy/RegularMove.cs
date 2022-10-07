using System.Collections.Generic;

namespace CheckersWpfGrid.Strategy;

public class RegularMove : Move
{
    public RegularMove(Figure figure, List<Cell> through, Cell to, MoveStrategy? toStrategy) : base(figure, through, to,
        toStrategy)
    {
    }

    public override int GetEatNumber()
    {
        // In any case?
        if (Figure.Cell != From)
            return -1;
        
        // Not on the same diagonal or not n-th cell in the path
        var toDist = From.DiagonalDist(To);
        if (toDist < 0 || toDist != Through.Count + 1)
            return -1;

        if (To.Figure != null)
            return -1;

        return Through.Count switch
        {
            1 => Through[0].Figure != null && Through[0].Figure!.Player != Figure.Player ? 1 : -1,
            0 => 0,
            _ => -1
        };
    }
}