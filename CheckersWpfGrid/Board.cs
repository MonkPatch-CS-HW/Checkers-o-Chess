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

            var cell = Figures[row * Size + column];
            if (cell == null || cell.Active == false || cell.Column != column || cell.Row != row)
                return null;
            return cell;
        }
        set
        {
            if (row < 0 || row >= Size || column < 0 || column >= Size)
                throw new IndexOutOfRangeException();

            Figures[row * Size + column] = value;
        }
    }
}