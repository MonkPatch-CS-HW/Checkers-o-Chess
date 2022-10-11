using System.Collections.Generic;
using System.Linq;
using CheckersWpfGrid.MoveStrategy.RussianChess;

namespace CheckersWpfGrid.MoveStrategy;

public abstract class MoveStrategy
{
    protected Ruleset Ruleset { get; }

    protected MoveStrategy(Ruleset ruleset)
    {
        Ruleset = ruleset;
    }
    
    public abstract string Name { get; }

    public abstract Move? GetMove(Figure figure, Cell cell);

    public MoveSet GetMoves(Figure figure)
    {
        var moveSet = new MoveSet(figure);
        foreach (var move in figure.Game.Table.Cells.Select(cell => GetMove(figure, cell)).Where(move => move != null))
        {
            moveSet.Add(move);
        }

        return moveSet;
    }
}