using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.MoveStrategy;

public abstract class Ruleset
{
    private readonly Dictionary<string, MoveStrategy> _cache = new();

    protected abstract MoveStrategy? CreateStrategy(string name);

    public MoveStrategy GetStrategy(string name)
    {
        if (_cache.ContainsKey(name))
            return _cache[name];
        var strategy = CreateStrategy(name);
        _cache[name] = strategy ?? throw new Exception($"Strategy {name} does not exist");
        return _cache[name]!;
    }

    public Player? CheckWinner(Game game)
    {
        var activePlayers = game.GetActivePlayers();
        if (activePlayers.Count > 1)
            return null;
        return activePlayers[0];
    }

    public Player GetCurrentPlayer(Game game)
    {
        if (game.LastMove == null) return game.Players[0];

        if (game.LastMove.EatenFigures.Count > 0 && game.LastMove.Figure.CanEat()) return game.LastMove.Figure.Player;

        var index = (game.Players.IndexOf(game.LastMove.Figure.Player) + 1) % game.Players.Count;
        return game.Players[index];
    }

    public bool CanSelectFigure(Figure figure)
    {
        if (figure.Game.LastMove != null && figure.Game.LastMove.EatenFigures.Count > 0 &&
            figure.Game.LastMove.Figure.CanEat())
            return figure == figure.Game.LastMove.Figure;
        return figure.Active && figure.Player == GetCurrentPlayer(figure.Game) && figure.CanMove();
    }

    public List<Figure> GetAvailableFigures(Game game)
    {
        return GetCurrentPlayer(game).Figures.Where(CanSelectFigure).ToList();
    }
}