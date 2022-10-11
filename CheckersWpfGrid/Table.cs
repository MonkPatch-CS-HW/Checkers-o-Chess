using System;
using System.Collections.Generic;

namespace CheckersWpfGrid;

public class Table
{
    public List<Cell> Cells { get; }
    public int Size { get; }

    public Table(int size)
    {
        Size = size;
        Cells = new List<Cell>(new Cell[Size * Size]);
    }

    public Cell this[int row, int column]
    {
        get
        {
            if (!Contains(row, column))
                throw new IndexOutOfRangeException();

            var cell = Cells[row * Size + column];
            return cell;
        }
        set
        {
            if (!Contains(row, column))
                throw new IndexOutOfRangeException();

            Cells[row * Size + column] = value;
        }
    }

    public bool Contains(int row, int column)
    {
        return row >= 0 && row < Size && column >= 0 && column < Size;
    }
}