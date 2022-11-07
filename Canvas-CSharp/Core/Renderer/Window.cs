using Canvas_CSharp.Core.Controls;
using Canvas_CSharp.Core.Utility;
using static SDL2.SDL;

namespace Canvas_CSharp.Core.Renderer;

public readonly struct Window
{
    public string Title { get; }
    public int ViewWidth { get; }
    public int ViewHeight { get; }
    public SDL_WindowFlags WindowFlags { get; }
    internal Renderer Renderer { get; } = null!;

    public Window(string title, uint viewWidth, uint viewHeight, SDL_WindowFlags windowFlags = 
        SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS)
    {
        Title = title;
        ViewWidth = (int)viewWidth;
        ViewHeight = (int)viewHeight;
        WindowFlags = windowFlags;
        Renderer = new Renderer(this);
    }
    
    public void RunApp<TState>(TState state, Func<Canvas, TState, Canvas> draw, Func<TState, Key, Option<TState>> onKeyPressed) where TState : notnull
    {
        Renderer.RunApp(Title, ViewWidth, ViewHeight, state, draw, onKeyPressed);
    }
    
    public void RunSimpleApp(Func<Canvas, Canvas> draw)
    {
        Renderer.RunApp(Title, ViewWidth, ViewHeight, (byte)0, (canvas, _) => draw(canvas), (_, _) => new Option<byte>().None());
    }
}