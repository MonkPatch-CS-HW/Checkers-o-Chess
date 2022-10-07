﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CheckersWpfGrid.Strategy;

namespace CheckersWpfGrid;

public partial class Figure : UserControl
{
    public readonly Game Game;

    public Figure(Game game)
    {
        InitializeComponent();
        Game = game;
        Strategy = new RegularStrategy(this, Game);
    }

    public Cell Cell
    {
        get => Game.Table[Row, Column];
        set
        {
            if (value.Figure != null)
                return;
            
            Row = value.Row;
            Column = value.Column;
            value.Figure = this;
        }
    }

    public Player Player
    {
        get => (Player)GetValue(PlayerProperty);
        init => SetValue(PlayerProperty, value);
    }

    public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(
        nameof(Player),
        typeof(Player),
        typeof(Figure));

    public bool Active
    {
        get => (bool)GetValue(ActiveProperty);
        set => SetValue(ActiveProperty, value);
    }

    private static readonly DependencyProperty ActiveProperty = DependencyProperty.Register(
        nameof(Active),
        typeof(bool),
        typeof(Figure),
        new PropertyMetadata(true));

    public MoveStrategy Strategy
    {
        get => (MoveStrategy)GetValue(StrategyProperty);
        // TODO: Make readonly
        set => SetValue(StrategyProperty, value);
    }

    private static readonly DependencyProperty StrategyProperty = DependencyProperty.Register(
        nameof(MoveStrategy),
        typeof(MoveStrategy),
        typeof(Figure));

    public int Column
    {
        get => (int)GetValue(ColumnProperty);
        set => SetValue(ColumnProperty, value);
    }

    public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register(
        nameof(Column),
        typeof(int),
        typeof(Figure));

    public int Row
    {
        get => (int)GetValue(RowProperty);
        set => SetValue(RowProperty, value);
    }

    public static readonly DependencyProperty RowProperty = DependencyProperty.Register(
        nameof(Row),
        typeof(int),
        typeof(Figure));

    public Brush Sprite
    {
        get => (Brush)GetValue(SpriteProperty);
        set => SetValue(SpriteProperty, value);
    }

    public static readonly DependencyProperty SpriteProperty = DependencyProperty.Register(
        nameof(Sprite),
        typeof(Brush),
        typeof(Figure));
}