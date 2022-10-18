using System;
using System.Collections.Generic;
using System.Linq;
using CheckersWpfGrid.MoveStrategy.RussianCheckers.GameState;
using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy;

public abstract class Ruleset
{
    private readonly Dictionary<string, MoveStrategy> _cache = new();

    public abstract string Name { get; }

    public abstract int DeckSize { get; }

    protected abstract MoveStrategy? CreateStrategy(string name);

    public MoveStrategy GetStrategy(string name)
    {
        if (_cache.ContainsKey(name))
            return _cache[name];
        var strategy = CreateStrategy(name);
        _cache[name] = strategy ?? throw new Exception($"Strategy {name} does not exist");
        return _cache[name]!;
    }

    public abstract GameState GetState(Game game);

    protected List<Player> GetActivePlayers(Game game)
    {
        return game.Players.Where(player => player.CanMove()).ToList();
    }

    protected virtual Player? CheckWinner(Game game)
    {
        var activePlayers = GetActivePlayers(game);
        if (activePlayers.Count > 1)
            return null;
        if (activePlayers.Count == 1)
            return activePlayers[0];
        return game.LastMove?.Figure.Player;
    }

    protected virtual Player? GetCurrentPlayer(Game game)
    {
        if (game.LastMove == null)
        {
            if (game.Players[0].Surrendered)
                return null;
            return game.Players[0];
        }

        var startIndex = game.Players.IndexOf(game.LastMove.Figure.Player) % game.Players.Count;
        for (var i = startIndex + 1; i < startIndex + game.Players.Count; i++)
        {
            var player = game.Players[i % game.Players.Count];
            if (player.CanMove())
                return player;
        }

        return null;
    }

    public abstract Figure? GetStartFigure(Cell cell);

    public virtual bool CanSelectFigure(Player player, Figure figure)
    {
        return figure.Player == player && figure.CanMove();
    }
}