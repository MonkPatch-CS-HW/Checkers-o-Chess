using CheckersWpfGrid.Players;

namespace CheckersWpfGrid.MoveStrategy;

public abstract class GameState
{
    public enum GameStateKind
    {
        Info,
        Finish,
    }

    protected GameState()
    {
    }
    
    public abstract Player? CurrentPlayer { get; }
    
    public abstract string Message { get; }
    
    public abstract GameStateKind Kind { get; }

    public abstract Player? Winner { get; }
}