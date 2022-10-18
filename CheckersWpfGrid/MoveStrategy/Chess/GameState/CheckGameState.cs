using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy.Chess.GameState;

public class CheckGameState : RegularGameState
{
    public Player Check { get; }
    public CheckGameState(Player currentPlayer, Player check) : base(currentPlayer)
    {
        Check = check;
    }

    public override string Message => $"{Check.Name} is under check; {CurrentPlayer.Name}'s move";
}