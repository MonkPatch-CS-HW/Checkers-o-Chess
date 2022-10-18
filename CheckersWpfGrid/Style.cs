namespace CheckersWpfGrid;

public class Style
{
    public Style(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public string Name { get; init; }
    public string Path { get; init; } 
}