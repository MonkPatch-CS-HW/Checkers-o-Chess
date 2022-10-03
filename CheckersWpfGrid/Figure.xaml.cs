using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CheckersWpfGrid;

public partial class Figure : UserControl
{
    public Figure()
    {
        InitializeComponent();
    }

    public Player? Player
    {
        get => (Player?)GetValue(PlayerProperty);
        set
        {
            SetValue(PlayerProperty, value);
            Active = value != null;
        }
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

    public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register(
        nameof(Active),
        typeof(bool),
        typeof(Figure),
        new PropertyMetadata(true));

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