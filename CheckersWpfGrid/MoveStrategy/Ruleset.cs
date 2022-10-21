using System;
using System.Collections.Generic;
using System.Linq;
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

    public virtual GameState GetState(Game game)
    {
        var currentPlayer = GetCurrentPlayer(game);
        if (currentPlayer == null)
            return new WinnerGameState(CheckWinner(game));

        return new RegularGameState(currentPlayer);
    }

    private List<Player> GetActivePlayers(Game game)
    {
        return game.Players.Where(IsActive).ToList();
    }

    protected virtual Player? CheckWinner(Game game)
    {
        var activePlayers = GetActivePlayers(game);
        return activePlayers.Count switch
        {
            > 1 => null,
            1 => activePlayers[0],
            _ => game.LastMove?.Figure.Player
        };
    }

    protected virtual Player GetFirstPlayer(Game game) => game.Players[1];

    protected virtual bool IsActive(Player player) => player.CanMove();

    protected virtual Player? GetCurrentPlayer(Game game)
    {
        var startIndex = game.LastMove == null
            ? game.Players.IndexOf(GetFirstPlayer(game))
            : game.Players.IndexOf(game.LastMove.Player) + 1;
        
        for (var i = startIndex; i < startIndex + game.Players.Count - 1; i++)
        {
            var player = game.Players[i % game.Players.Count];
            if (IsActive(player))
                return player;
        }

        return null;
    }

    public abstract bool ShouldEat { get; }

    public abstract Figure? GetStartFigure(Cell cell);

    public virtual bool CanSelectFigure(Player player, Figure figure)
    {
        if (figure.Player != player)
            return false;
        if (ShouldEat && player.Figures.Any(own => own.CanEat()))
            return figure.CanEat();
        return figure.CanMove();
    }
}