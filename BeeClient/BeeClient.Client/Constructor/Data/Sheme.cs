using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace B2bContructor.Client.Ctor.Data;


public class BaseElement
{
    public int Id { get; set; }
    
    public int Width { get; set; }
    public int Height { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
}

public class TextElement: BaseElement
{
    public string Text { get; set; }
    public int FontSize { get; set; } = 40;
    public Color Color { get; set; } = Color.Green;
    public Color BackgroundColor { get; set; } = Color.Red;
}

public class ImageElement : BaseElement
{
    public string Url { get; set; }
}

public class BoundingClientRect
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Top { get; set; }
    public double Right { get; set; }
    public double Bottom { get; set; }
    public double Left { get; set; }
}


public class ConsructorConfig
{
    public int Width = 800;
    public int Height = 800;

}
