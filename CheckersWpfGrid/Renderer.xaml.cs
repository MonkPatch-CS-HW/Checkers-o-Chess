using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CheckersWpfGrid.MoveStrategy;

namespace CheckersWpfGrid;

public partial class Renderer : Window
{
    public Game Game { get; }
    protected Highlighter Highlighter { get; }

    public Figure? SelectedFigure { get; private set; }
    public MoveSet? AvailableMoves => SelectedFigure?.Strategy.GetMoves(SelectedFigure);
    public List<Figure>? AvailableFigures => Game.State.CurrentPlayer?.GetAvailableFigures();

    public Renderer(Game game, Style style)
    {
        Game = game;
        Highlighter = new Highlighter();
        InitializeComponent();
        SelectStyle(style);

        FillRowsAndColumns();
        RenderTable(game.Table);
        BindTable(game.Table);
        RenderBoard(game.Board);
        BindFigures(game.Board);

        game.AfterMove += GameOnAfterMove;
        Highlighter.HighlightFigures(AvailableFigures);
    }

    private void SelectStyle(Style style)
    {
        var uri = new Uri(style.Path, UriKind.Relative);
        if (Application.LoadComponent(uri) is not ResourceDictionary resourceDict)
            return;
        Application.Current.Resources.Clear();
        Application.Current.Resources.MergedDictionaries.Add(resourceDict);
    }

    private void GameOnAfterMove(Move move)
    {
        Highlighter.ClearHighlighting();
        Highlighter.HighlightTrace(Game.LastMove);
        Highlighter.HighlightFigures(AvailableFigures);
    }

    private void RenderTable(Table table)
    {
        foreach (var cell in table.Cells)
            Grid.Children.Add(cell);
    }

    private void RenderBoard(Board board)
    {
        foreach (var figure in board.Figures.Where(figure => figure != null))
            Grid.Children.Add(figure!);
    }

    private void FillRowsAndColumns()
    {
        for (var i = 0; i < Game.Table.Size; i++)
        {
            Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.RowDefinitions.Add(new RowDefinition());
        }
    }

    private void BindTable(Table table)
    {
        foreach (var cell in table.Cells)
        {
            var source = new RelativeSource(RelativeSourceMode.Self);
            var bindColumn = new Binding("Column") { RelativeSource = source };
            var bindRow = new Binding("Row") { RelativeSource = source };
            cell.SetBinding(Grid.ColumnProperty, bindColumn);
            cell.SetBinding(Grid.RowProperty, bindRow);
            cell.MouseDown += CellOnMouseDown;
        }
    }

    private void CellOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        SelectCell((Cell)sender);
    }

    private void BindFigures(Board board)
    {
        foreach (var figure in board.Figures)
        {
            if (figure == null)
                continue;
            var source = new RelativeSource(RelativeSourceMode.Self);
            var bindColumn = new Binding("Column") { RelativeSource = source };
            var bindRow = new Binding("Row") { RelativeSource = source };
            figure.SetBinding(Grid.ColumnProperty, bindColumn);
            figure.SetBinding(Grid.RowProperty, bindRow);
            figure.MouseDown += FigureOnMouseDown;
        }
    }

    private void FigureOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (SelectedFigure != null && (Figure)sender != SelectedFigure)
        {
            SelectCell(((Figure)sender).Cell);
            return;
        }

        SelectFigure((Figure)sender);
    }

    private void SelectCell(Cell cell)
    {
        if (Game.CurrentPlayer is { IsBot: true }) return;
        var move = AvailableMoves?.GetMoveByDestination(cell);
        Game.CommitMove(move);
        SelectedFigure = null;
    }

    private void SelectFigure(Figure figure)
    {
        if (Game.CurrentPlayer is not { IsBot: false } || AvailableFigures == null ||
            !AvailableFigures.Contains(figure)) return;
        Highlighter.ClearHighlighting();
        Highlighter.HighlightTrace(Game.LastMove);
        Highlighter.HighlightFigures(AvailableFigures);
        if (figure == SelectedFigure)
        {
            SelectedFigure = null;
            return;
        }

        SelectedFigure = figure;
        Highlighter.HighlightMoves(AvailableMoves);
    }

    private void OnSurrenderBtnClick(object sender, RoutedEventArgs e)
    {
        if (Game.CurrentPlayer is { IsBot: false })
            Game.CurrentPlayer.Surrender();
    }
}