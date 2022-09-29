using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CheckersWpfGrid;

public partial class Figure : UserControl
{
    public Cell? Cell
    {
        get => (Cell?)GetValue(CellProperty);
        set
        {
            if (Cell?.Figure?.Cell != null)
                Cell.Figure.Cell = null;
            if (value != null)
                value.Figure = this;
            SetValue(CellProperty, value);
        }
    }

    public static readonly DependencyProperty CellProperty = DependencyProperty.Register(
        nameof(Cell),
        typeof(Cell),
        typeof(Figure),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.AffectsRender));

    public Brush Sprite
    {
        get => (Brush)GetValue(SpriteProperty);
        set => SetValue(SpriteProperty, value);
    }

    public static readonly DependencyProperty SpriteProperty = DependencyProperty.Register(
        nameof(Sprite),
        typeof(Brush),
        typeof(Figure));

    public Figure()
    {
        InitializeComponent();
    }
}