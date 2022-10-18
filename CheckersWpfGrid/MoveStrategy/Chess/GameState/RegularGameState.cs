using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy.Chess.GameState;

public class RegularGameState : CheckersWpfGrid.MoveStrategy.GameState
{
    public RegularGameState(Player currentPlayer) : base()
    {
        CurrentPlayer = currentPlayer;
    }

    public override string Message => $"{CurrentPlayer.Name}'s move";
    public override GameStateKind Kind => GameStateKind.Info;
    public override Player? Winner => null;
    public override Player CurrentPlayer { get; }
}