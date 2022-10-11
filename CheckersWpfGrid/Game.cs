using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid;

public sealed class Game
{
    public Game(Ruleset ruleset)
    {
        Ruleset = ruleset;
        Table = CreateTable();
        Players = CreatePlayers();
        Board = CreateBoard();
        Highlighter = new Highlighter(this);
        AvailableFigures = Ruleset.GetAvailableFigures(this);
        Highlighter.HighlightFigures(AvailableFigures);
    }

    public Board Board { get; }
    public Table Table { get; }
    public List<Player> Players { get; }
    private MoveSet? MoveSet { get; set; }
    public Ruleset Ruleset { get; }
    public List<Move> History { get; } = new();
    private Highlighter Highlighter { get; }

    private List<Figure> AvailableFigures { get; set; }

    public Move? LastMove => History.Count > 0 ? History[^1] : null;

    private List<Player> CreatePlayers()
    {
        var first = new BlackPlayer(this);
        var second = new WhitePlayer(this);
        return new List<Player> { first, second };
    }

    private Table CreateTable()
    {
        var table = new Table();
        for (var r = 0; r < 8; r++)
        for (var c = 0; c < 8; c++)
        {
            var color = (c + r) % 2 == 1 ? Cell.CellKind.Black : Cell.CellKind.White;
            var cell = new Cell(this) { Kind = color, Row = r, Column = c };
            cell.MouseDown += CellOnMouseDown;
            table[r, c] = cell;
        }

        return table;
    }

    private Board CreateBoard()
    {
        var board = new Board();
        for (var r = 0; r < 8; r++)
        for (var c = 0; c < 8; c++)
            foreach (var player in Players)
            {
                var figure = player.GetStartFigure(Table[r, c]);
                if (figure == null)
                    continue;
                figure.MouseDown += FigureOnMouseDown;
                board[r, c] = figure;
            }

        return board;
    }

    private void CellOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var cell = (Cell)sender;
        SelectCell(cell);
    }

    private void FigureOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var figure = (Figure)sender;
        SelectFigure(figure);
    }

    private bool SelectCell(Cell cell)
    {
        var move = MoveSet?.GetMoveByDestination(cell);
        if (move == null) return false;
        move.Execute();
        History.Add(move);
        MoveSet = null;
        AvailableFigures = Ruleset.GetAvailableFigures(this);
        Highlighter.ClearHighlighting().HighlightFigures(AvailableFigures);
        return true;
    }

    public bool SelectFigure(Figure? figure)
    {
        Highlighter.ClearHighlighting().HighlightFigures(AvailableFigures);
        if (figure == null)
            return true;
        if (figure == MoveSet?.Figure)
        {
            MoveSet = null;
            return true;
        }

        if (!Ruleset.CanSelectFigure(figure))
            return false;
        MoveSet = figure.Strategy.GetMoves(figure);
        Highlighter.HighlightMoveSet(MoveSet);
        return true;
    }

    public List<Player> GetActivePlayers()
    {
        return Players.Where(player => player.CanMove()).ToList();
    }
}