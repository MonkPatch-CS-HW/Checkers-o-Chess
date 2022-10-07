using System.Collections.Generic;

namespace CheckersWpfGrid.Strategy;

public class RegularMoveBuilder : MoveBuilder
{
    public RegularMoveBuilder(Figure figure) : base(figure)
    {
    }

    protected override Move CreateMove(Figure figure, List<Cell> through, Cell to, MoveStrategy? toStrategy)
    {
        return new RegularMove(figure, through, to, toStrategy);
    }
    
    // public void Fill
}