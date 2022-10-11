using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CheckersWpfGrid;

public abstract class Player
{
    public enum PlayerKind
    {
        White,
        Black
    }

    protected Game Game { get; }
    public List<Figure> Figures { get; } = new List<Figure>();
    
    protected Player(Game game)
    {
        Game = game;
    }
    
    public abstract string Name { get; }
    
    public abstract PlayerKind Kind { get; }

    public bool IsEnemy(Player player) => true;

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