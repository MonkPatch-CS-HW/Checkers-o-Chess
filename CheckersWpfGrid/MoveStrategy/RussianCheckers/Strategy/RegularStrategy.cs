namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.Strategy;

public class RegularStrategy : BaseStrategy
{
    public RegularStrategy(Ruleset ruleset) : base(ruleset)
    {
    }


    public override string Name => "Regular";

    public override Move? GetMove(Figure figure, Cell destination)
    {
        if (figure.Cell.DiagonalDist(destination) > 2)
            return null;
        if (figure.Cell.DiagonalDist(destination) == 2)
        {
            var first = figure.Cell.Relative(figure.Cell.Direction(destination));
            if (first?.Figure == null || !figure.Player.IsEnemy(first.Figure.Player))
                return null;
        }

        if (figure.Cell.DiagonalDist(destination) == 1)
        {
            var dirRow = figure.Cell.Direction(destination).DirRow;
            switch (dirRow)
            {
                case < 0 when figure.Player == figure.Game.Players[0]:
                case > 0 when figure.Player == figure.Game.Players[1]:
                    return null;
            }
        }
        
        var move = base.GetMove(figure, destination);
        if (move == null || !figure.Player.CheckOppositeBorder(destination)) return move;
        if (move.Figure.Strategy.Name == "Miss") return move;
        move.OnExecute += OnBorderExecute;
        move.OnUndo += OnBorderUndo;

        return move;
    }

    private void OnBorderExecute(Figure figure) => figure.Strategy = Ruleset.GetStrategy("Miss");

    private void OnBorderUndo(Figure figure) => figure.Strategy = Ruleset.GetStrategy("Regular");
}