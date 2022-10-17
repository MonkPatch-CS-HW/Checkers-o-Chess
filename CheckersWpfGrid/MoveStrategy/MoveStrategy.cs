using System.Linq;

namespace CheckersWpfGrid.MoveStrategy;

public abstract class MoveStrategy
{
    protected MoveStrategy(Ruleset ruleset)
    {
        Ruleset = ruleset;
    }

    protected Ruleset Ruleset { get; }

    public abstract string Name { get; }

    public abstract Move? GetMove(Figure figure, Cell destination);

    public MoveSet GetMoves(Figure figure)
    {
        var moveSet = new MoveSet(figure);
        
        if (figure.Active == false)
            return moveSet;
        
        foreach (var move in figure.Game.Table.Cells.Select(cell => GetMove(figure, cell)).Where(move => move != null))
            moveSet.Add(move);

        return moveSet;
    }
}