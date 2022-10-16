using System.Collections.Generic;
using System.Linq;
using CheckersWpfGrid.MoveStrategy;

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
    public virtual bool IsBot => false;

    public abstract PlayerKind Kind { get; }

    public virtual bool IsEnemy(Player player)
    {
        return player != this;
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
    
    public bool Surrendered { get; private set; }


    public bool CanSelectFigure(Figure figure)
    {
        return figure.Player == this && figure.CanMove();
    }

    public List<Figure> GetAvailableFigures()
    {
        return Figures.Where(CanSelectFigure).ToList();
    }

    public void Surrender()
    {
        Surrendered = true;
        Game.UpdateState();
    }

    public bool CanMove()
    {
        return !Surrendered && Figures.Any(figure => figure.CanMove());
    }
}