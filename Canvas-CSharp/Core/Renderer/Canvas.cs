namespace Canvas_CSharp.Core.Renderer;

public readonly struct Canvas
{
    public int ViewWidth { get; }
    public int ViewHeight { get; }
    
    internal Renderer Renderer { get; }

    internal Canvas(int viewWidth, int viewHeight, Renderer renderer)
    {
        ViewWidth = viewWidth;
        ViewHeight = viewHeight;
        Renderer = renderer;
    }
    
    public Canvas(int viewWidth, int viewHeight, Window window)
    {
        ViewWidth = viewWidth;
        ViewHeight = viewHeight;
        Renderer = window.Renderer;
    }
}