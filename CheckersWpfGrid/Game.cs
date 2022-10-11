using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CheckersWpfGrid.MoveStrategy;
using CheckersWpfGrid.MoveStrategy.RussianChess.Miss;

namespace CheckersWpfGrid;

public sealed class Game
{
    public readonly Board Board;
    public readonly Table Table;
    public readonly List<Player> Players;
    private readonly List<Cell> _selectedCells = new List<Cell>();
    private MoveSet? _moveSet;
    public Ruleset Ruleset;

    public Game(Ruleset ruleset)
    {
        Ruleset = ruleset;
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
                var color = (c + r) % 2 == 1 ? Cell.CellKind.Black : Cell.CellKind.White;
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
        var move = _moveSet?.GetMoveByDestination(cell);
        if (move == null) return;
        move.Execute();
        _moveSet?.ClearHighlighting();
        _moveSet = null;
    }

    private List<Player> CreatePlayers()
    {
        var first = new BlackPlayer(this);
        var second = new WhitePlayer(this);
        return new List<Player> { first, second };
    }

    private Board CreateBoard()
    {
        var board = new Board();
        for (var r = 0; r < 8; r++)
        {
            for (var c = 0; c < 8; c++)
            {
                foreach (var player in Players)
                {
                    var figure = player.GetStartFigure(Table[r, c]);
                    if (figure == null)
                        continue;
                    figure.MouseDown += FigureOnMouseDown;
                    board[r, c] = figure;
                }
            }
        }

        return board;
    }

    private void FigureOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var figure = (Figure)sender;
        SelectFigure(figure);
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
        _moveSet?.ClearHighlighting();
        if (figure != null && figure != _moveSet?.Figure)
            _moveSet = figure.Strategy.GetMoves(figure);
        _moveSet?.HighlightCells();
        return true;
    }
}