using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.Strategy;

public abstract class MoveStrategy
{
    public enum StrategyKind
    {
        Regular,
        Miss,
    }

    protected readonly Figure Figure;
    protected readonly Game Game;

    public abstract StrategyKind Kind { get; }

    protected MoveStrategy(Figure figure, Game game)
    {
        Figure = figure;
        Game = game;
    }

    public abstract MoveSet GetMoves();
}