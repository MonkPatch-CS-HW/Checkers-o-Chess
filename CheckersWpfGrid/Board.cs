using System;
using System.Collections.Generic;

namespace CheckersWpfGrid;

public class Board
{
    public List<Figure?> Figures = new List<Figure?>(new Figure?[64]);

    public Figure? this[int row, int column]
    {
        get
        {
            if (row is (< 0 or >= 8) || column is (< 0 or >= 8))
                return null;

            var cell = Figures[row * 8 + column];
            if (cell == null || cell.Active == false || cell.Column != column || cell.Row != row)
                return null;
            return cell;
        }
        set
        {
            if (row is (< 0 or >= 8) || column is (< 0 or >= 8))
                throw new IndexOutOfRangeException();

            Figures[row * 8 + column] = value;
        }
    }
}