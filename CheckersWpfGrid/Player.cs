using System.Windows.Media;

namespace CheckersWpfGrid;

public class Player
{
    public enum PlayerKind
    {
        White,
        Black
    }

    public Player(PlayerKind kind)
    {
        Kind = kind;
    }
    
    public PlayerKind Kind { get; init; }
}