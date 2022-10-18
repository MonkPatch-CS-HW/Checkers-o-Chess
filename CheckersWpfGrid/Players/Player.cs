using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.Players;

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

    public Game Game { get; }
    public List<Figure> Figures { get; } = new();

    public abstract string Name { get; }
    public virtual bool IsBot => false;

    public abstract PlayerKind Kind { get; }

    public virtual bool IsEnemy(Player player)
    {
        return player != this;
    }

    public virtual Figure? AddFigure(Cell cell, string strategy)
    {
        var figure = new Figure(Game, this, Game.Ruleset.GetStrategy(strategy))
        {
            Row = cell.Row,
            Column = cell.Column
        };
        Figures.Add(figure);
        return figure;
    }

    public abstract bool CheckOppositeBorder(Cell cell);

    public bool Surrendered { get; private set; }

    public List<Figure> GetAvailableFigures()
    {
        return Figures.Where(figure => Game.Ruleset.CanSelectFigure(this, figure)).ToList();
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