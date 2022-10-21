using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

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

        var moves = figure.Game.Table.Cells.Select(cell => GetMove(figure, cell)).Where(move => move != null).ToList()!;

        var eatingMoves = moves.Where(move => move!.Eats).ToList();
        if (eatingMoves.Count > 0 && Ruleset.ShouldEat)
            moveSet.AddRange(eatingMoves!);
        else
            moveSet.AddRange(moves!);

        return moveSet;
    }
}