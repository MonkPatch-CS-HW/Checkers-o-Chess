using System.Windows.Media;

namespace CheckersWpfGrid;

public abstract class Player
{
    public abstract Brush Sprite { get; }

    public abstract bool CheckStartPosition(int row, int column);
}