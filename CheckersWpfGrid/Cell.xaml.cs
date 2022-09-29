using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CheckersWpfGrid;

public partial class Cell : UserControl
{
    public int Row
    {
        get => (int)GetValue(RowProperty);
        init => SetValue(RowProperty, value);
    }

    public static readonly DependencyProperty RowProperty = DependencyProperty.Register(
        nameof(Row),
        typeof(int),
        typeof(Cell));

    public int Column
    {
        get => (int)GetValue(ColumnProperty);
        init => SetValue(ColumnProperty, value);
    }

    public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register(
        nameof(Column),
        typeof(int),
        typeof(Cell));

    public Figure? Figure
    {
        get
        {
            var figure = (Figure?)GetValue(FigureProperty);
            if (figure?.Cell == this)
                return figure;
            Figure = null;
            return null;
        }
        set => SetValue(FigureProperty, value);
    }

    public static readonly DependencyProperty FigureProperty = DependencyProperty.Register(
        nameof(Figure),
        typeof(Figure),
        typeof(Cell),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender));

    public Brush Color
    {
        get => (Brush)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(Brush),
        typeof(Cell),
        new PropertyMetadata(Brushes.White));

    // public void Move()

    public Cell()
    {
        InitializeComponent();
    }
}