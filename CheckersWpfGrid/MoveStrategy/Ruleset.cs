using System;
using System.Collections.Generic;

namespace CheckersWpfGrid.MoveStrategy;

public abstract class Ruleset {

    private readonly Dictionary<string, MoveStrategy> _cache =
        new Dictionary<string, MoveStrategy>();

    protected abstract MoveStrategy? CreateStrategy(string name);

    public MoveStrategy GetStrategy(string name)
    {
        if (_cache.ContainsKey(name))
            return _cache[name];
        var strategy = CreateStrategy(name);
        _cache[name] = strategy ?? throw new Exception($"Strategy {name} does not exist");
        return _cache[name]!;
    }
}