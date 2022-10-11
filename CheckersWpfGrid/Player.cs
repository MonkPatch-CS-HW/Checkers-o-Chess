using System.Windows.Media;

namespace CheckersWpfGrid;

public abstract class Player
{
    public enum PlayerKind
    {
        White,
        Black
    }

    protected Game Game { get; }
    
    protected Player(Game game)
    {
        Game = game;
    }
    
    public abstract string Name { get; }
    
    public abstract PlayerKind Kind { get; }

    public bool IsEnemy(Player player) => true;

    public abstract Figure? GetStartFigure(Cell cell);

    public abstract bool CheckOppositeBorder(Cell cell);
}