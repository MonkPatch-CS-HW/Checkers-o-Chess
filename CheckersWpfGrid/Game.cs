using System;
using System.Collections.Generic;
using System.Linq;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid;

public sealed class Game
{
    public Game(Ruleset ruleset, bool withBot = false)
    {
        Ruleset = ruleset;
        Table = CreateTable();
        Players = CreatePlayers(withBot);
        Board = CreateBoard();
    }

    public Board Board { get; }
    public Table Table { get; }
    public List<Player> Players { get; }
    public Ruleset Ruleset { get; }
    public List<Move> History { get; } = new();
    public Move? LastMove => History.Count > 0 ? History[^1] : null;
    public Player? Winner { get; private set; }

    public Figure? SelectedFigure { get; private set; }

    public Player CurrentPlayer => Ruleset.GetCurrentPlayer(this);
    public MoveSet? AvailableMoves => SelectedFigure?.Strategy.GetMoves(SelectedFigure);

    public List<Figure> AvailableFigures => CurrentPlayer.GetAvailableFigures();

    public event Action<Move>? AfterMove;
    public event Action<Figure>? AfterSelectFigure;
    public event Action<Player>? AfterWin;

    private List<Player> CreatePlayers(bool withBot = false)
    {
        return new List<Player>
        {
            new BlackPlayer(this),
            withBot ? new UniversalWhiteBot(this) : new WhitePlayer(this)
        };
    }

    private Table CreateTable()
    {
        var table = new Table(Ruleset.DeckSize);
        for (var r = 0; r < table.Size; r++)
        for (var c = 0; c < table.Size; c++)
        {
            var color = (c + r) % 2 == 1 ? Cell.CellKind.Black : Cell.CellKind.White;
            var cell = new Cell(this) { Kind = color, Row = r, Column = c };
            table[r, c] = cell;
        }

        return table;
    }

    private Board CreateBoard()
    {
        var board = new Board(Ruleset.DeckSize);
        for (var r = 0; r < board.Size; r++)
        for (var c = 0; c < board.Size; c++)
            foreach (var player in Players)
            {
                var figure = player.GetStartFigure(Table[r, c]);
                if (figure == null)
                    continue;
                board[r, c] = figure;
            }

        return board;
    }

    public bool CommitMove(Move? move)
    {
        if (move == null || move.Figure.Player != CurrentPlayer) return false;
        move.Execute();
        History.Add(move);
        SelectedFigure = null;
        AfterMove?.Invoke(move);
        return true;
    }

    public bool MoveActiveFigureTo(Cell cell)
    {
        var move = AvailableMoves?.GetMoveByDestination(cell);
        return CommitMove(move);
    }

    public bool SelectFigure(Figure? figure)
    {
        if (figure == null)
        {
            SelectedFigure = null;
            return true;
        }

        if (!CurrentPlayer.CanSelectFigure(figure))
            return false;
        
        SelectedFigure = figure;
        AfterSelectFigure?.Invoke(figure);
        return true;
    }

    public void Surrender(Player player)
    {
        player.Surrender();
        var winner = CheckWinner();
        if (winner == null) return;
        Winner = winner;
        AfterWin?.Invoke(winner);
    }

    public Player? CheckWinner() => Ruleset.CheckWinner(this);
}