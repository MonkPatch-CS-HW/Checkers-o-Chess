using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CheckersWpfGrid.Strategy;

namespace CheckersWpfGrid;

public sealed class Game
{
    public readonly Board Board;
    public readonly Table Table;
    public readonly List<Player> Players;
    private readonly List<Cell> _selectedCells = new List<Cell>();
    private MoveSet? _moveSet;
    private Figure? _selectedFigure; 

    public Game()
    {
        Table = CreateTable();
        Players = CreatePlayers();
        Board = CreateBoard();
    }

    private Table CreateTable()
    {
        var table = new Table();
        for (var r = 0; r < 8; r++)
        {
            for (var c = 0; c < 8; c++)
            {
                var color = (c + r) % 2 == 0 ? Cell.CellKind.Black : Cell.CellKind.White;
                var cell = new Cell(this) { Kind = color, Row = r, Column = c };
                cell.MouseDown += CellOnMouseDown;
                table[r, c] = cell;
            }
        }

        return table;
    }

    private void CellOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var cell = (Cell)sender;
        // _selectedFigure?.Move(cell.Row, cell.Column);
        // ClearCellsSelection();
        // SelectFigure(null);
        var move = _moveSet.GetMoveByDestination(cell);
        if (move == null) return;
        move.Execute();
        _moveSet.ClearHighlighting();
    }

    private List<Player> CreatePlayers()
    {
        var first = new Player(Player.PlayerKind.Black);
        var second = new Player(Player.PlayerKind.White);
        return new List<Player> { first, second };
    }

    private Board CreateBoard()
    {
        var board = new Board();
        for (var r = 0; r < 8; r++)
        {
            for (var c = 0; c < 8; c++)
            {
                if ((c + r) % 2 != 0)
                    continue;
                if (r is > 2 and < 5)
                    continue;

                var playerIndex = r > 3 ? 1 : 0;
                var player = Players[playerIndex];
                var figure = new Figure(this) { Column = c, Row = r, Player = player };
                figure.MouseDown += FigureOnMouseDown;
                board[r, c] = figure;
            }
        }

        return board;
    }

    private void FigureOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var figure = (Figure)sender;
        // SelectFigure(figure);
        _moveSet?.ClearHighlighting();
        _moveSet = figure.Strategy.GetMoves();
        // foreach (var move in moves)
        _moveSet.HighlightCells();
    }

    private void ClearCellsSelection()
    {
        foreach (var cell in _selectedCells.ToList())
        {
            // cell.Highlighted = false;
            _selectedCells.Remove(cell);
        }
    }

    private void SelectCells(List<Cell>? cells)
    {
        ClearCellsSelection();
        if (cells == null)
            return;
        
        foreach (var cell in cells)
        {
            // cell.Highlighted = true;
            _selectedCells.Add(cell);
        }
    }

    public bool SelectFigure(Figure? figure)
    {
        // if (figure == _selectedFigure)
        //     return SelectFigure(null);
        //
        // if (figure == null)
        // {
        //     _selectedFigure = null;
        //     SelectCells(null);
        //     return true;
        // }
        //
        // _selectedFigure = figure;
        // SelectCells(figure.Strategy.GetMoves());
        return true;
    }
}