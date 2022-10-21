using System.Linq;
using CheckersWpfGrid.MoveStrategy.Corners.Strategy;
using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy.Corners;

public class CornersRuleset : Ruleset
{
    public override string Name => "Corners";
    public override int DeckSize => 8;
    protected override MoveStrategy? CreateStrategy(string name)
    {
        return name switch
        {
            "Regular" => new RegularStrategy(this),
            _ => null,
        };
    }

    public override GameState GetState(Game game)
    {
        var winner = CheckWinner(game);
        return winner != null ? new WinnerGameState(winner) : base.GetState(game);
    }

    protected override Player? CheckWinner(Game game)
    {
        if (game.Players[0].Figures.All(figure => figure.Cell is { Row: >= 6 and <= 8, Column: >= 6 and <= 8 }))
            return game.Players[0];
        if (game.Players[1].Figures.All(figure => figure.Cell is { Row: >= 0 and <= 2, Column: >= 0 and <= 2 }))
            return game.Players[1];
        return null;
    }

    public override bool ShouldEat => false;
    public override Figure? GetStartFigure(Cell cell)
    {
        return (cell.Row, cell.Column) switch
        {
            (>= 0 and <= 2, >= 0 and <= 2) => cell.Game.Players[0].AddFigure(cell, "Regular"),
            (>= 5 and <= 7, >= 5 and <= 7) => cell.Game.Players[1].AddFigure(cell, "Regular"),
            _ => null,
        };
    }
}