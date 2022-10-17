using System;
using System.Collections.Generic;

namespace CheckersWpfGrid;

public class Board
{
    public List<Figure?> Figures { get; }
    public int Size { get; }

    public Board(int size)
    {
        Size = size;
        Figures = new List<Figure?>(new Figure?[size * size]);
    }

    public Figure? this[int row, int column]
    {
        get
        {
            if (row < 0 || row >= Size || column < 0 || column >= Size)
                return null;

            var figure = Figures[row * Size + column];
            if (figure == null || figure.Active == false || figure.Column != column || figure.Row != row)
                return null;
            return figure;
        }
        set
        {
            if (row < 0 || row >= Size || column < 0 || column >= Size)
                throw new IndexOutOfRangeException();

            Figures[row * Size + column] = value;
        }
    }
}