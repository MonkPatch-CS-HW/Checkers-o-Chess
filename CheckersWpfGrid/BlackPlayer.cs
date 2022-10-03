using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersWpfGrid;

public class BlackPlayer : Player
{
    public override Brush Sprite { get; } = new ImageBrush(new BitmapImage(new Uri("C:/Users/Wynell/RiderProjects/CheckersWpfGrid/CheckersWpfGrid/Images/FigureBlack.png")));

    public override bool CheckStartPosition(int row, int column)
    {
        if ((column + row) % 2 != 0)
            return false;
        return row is >= 5;
    }
}