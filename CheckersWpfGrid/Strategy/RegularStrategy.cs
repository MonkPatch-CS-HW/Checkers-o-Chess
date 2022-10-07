using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.Strategy;

public class RegularStrategy : MoveStrategy
{
    public RegularStrategy(Figure figure, Game game) : base(figure, game)
    {
    }

    public override StrategyKind Kind => StrategyKind.Regular;

    public override MoveSet GetMoves()
    {
        var moves = new MoveSet(Figure)
        {
            new RegularMoveBuilder(Figure).ToRelative(1, 1).TryBuild(),
            new RegularMoveBuilder(Figure).ToRelative(1, -1).TryBuild(),
            new RegularMoveBuilder(Figure).ToRelative(-1, 1).TryBuild(),
            new RegularMoveBuilder(Figure).ToRelative(-1, -1).TryBuild(),
            
            new RegularMoveBuilder(Figure).ThroughRelative(1, 1).ToRelative(1, 1).TryBuild(),
            new RegularMoveBuilder(Figure).ThroughRelative(1, -1).ToRelative(1, -1).TryBuild(),
            new RegularMoveBuilder(Figure).ThroughRelative(-1, 1).ToRelative(-1, 1).TryBuild(),
            new RegularMoveBuilder(Figure).ThroughRelative(-1, -1).ToRelative(-1, -1).TryBuild(),
        };
        return moves;
    }
}