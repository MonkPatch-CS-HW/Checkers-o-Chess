﻿using System;
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
    
    public List<Player> GetActivePlayers(Game game)
    {
        return game.Players.Where(player => player.CanMove()).ToList();
    }

    public Player? CheckWinner(Game game)
    {
        var activePlayers = GetActivePlayers(game);
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
}