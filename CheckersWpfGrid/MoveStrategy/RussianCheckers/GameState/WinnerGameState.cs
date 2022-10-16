namespace CheckersWpfGrid.MoveStrategy.RussianCheckers.GameState;

public class WinnerGameState : CheckersWpfGrid.MoveStrategy.GameState
{
    public WinnerGameState(Player? winner) : base()
    {
        Winner = winner;
    }

    public override string Message => $"{Winner?.Name ?? "No one"} has won";
    public override GameStateKind Kind => GameStateKind.Finish;
    public override Player? CurrentPlayer => null;
    public override Player? Winner { get; }
}