using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid;

public abstract class Player
{
    public enum PlayerKind
    {
        White,
        Black
    }

    protected Player(Game game)
    {
        Game = game;
    }

    protected Game Game { get; }
    public List<Figure> Figures { get; } = new();

    public abstract string Name { get; }

    public abstract PlayerKind Kind { get; }

    public bool IsEnemy(Player player)
    {
        return true;
    }

    public Figure? GetStartFigure(Cell cell)
    {
        var figure = CreateFigure(cell);
        if (figure != null)
            Figures.Add(figure);
        return figure;
    }

    protected abstract Figure? CreateFigure(Cell cell);

    public abstract bool CheckOppositeBorder(Cell cell);

    public bool CanMove()
    {
        return Figures.Any(figure => figure.CanMove());
    }
}