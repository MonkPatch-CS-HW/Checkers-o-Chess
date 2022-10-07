using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersWpfGrid.Strategy;

public abstract class MoveBuilder
{
    private readonly Figure _figure;
    private readonly List<Cell> _through = new List<Cell>();
    private MoveStrategy? _toStrategy;
    private Cell? _to;

    public MoveBuilder(Figure figure)
    {
        _figure = figure;
    }

    protected abstract Move CreateMove(Figure figure, List<Cell> through, Cell to, MoveStrategy? toStrategy);

    public MoveBuilder Through(Cell cell)
    {
        _through.Add(cell);
        return this;
    }

    public MoveBuilder To(Cell cell)
    {
        _to = cell;
        return this;
    }

    public Cell LastCellInPath => _through.Count > 0 ? _through.Last() : _figure.Cell;

    public MoveBuilder ToRelative(int distRow, int distColumn)
    {
        var to = LastCellInPath.Relative(distRow, distColumn);
        if (to != null)
            To(to);
        return this;
    }

    public MoveBuilder ThroughRelative(int distRow, int distColumn)
    {
        var through = LastCellInPath.Relative(distRow, distColumn);
        if (through != null)
            Through(through);
        return this;
    }

    public MoveBuilder Strategy(MoveStrategy strategy)
    {
        _toStrategy = strategy;
        return this;
    }

    public Move Build()
    {
        if (_to == null)
            throw new Exception("Destination cell is not set");
        var move = CreateMove(_figure, _through, _to, _toStrategy);
        return move;
    }

    public Move? TryBuild()
    {
        try
        {
            return Build();
        }
        catch (Exception e)
        {
            return null;
        }
    }
}