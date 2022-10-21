using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy;

public class WinnerGameState : GameState
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