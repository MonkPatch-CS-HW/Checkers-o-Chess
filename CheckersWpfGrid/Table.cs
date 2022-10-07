using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace CheckersWpfGrid;

public class Table
{
    public List<Cell> Cells = new List<Cell>(new Cell[64]);
    
    public Cell this[int row, int column]
    {
        get
        {
            if (!Contains(row, column))
                throw new IndexOutOfRangeException();

            var cell = Cells[row * 8 + column];
            return cell;
        }
        set
        {
            if (!Contains(row, column))
                throw new IndexOutOfRangeException();

            Cells[row * 8 + column] = value;
        }
    }

    public bool Contains(int row, int column)
    {
        return row is >= 0 and < 8 && column is >= 0 and < 8;
    }
    
}