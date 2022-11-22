using Canvas_CSharp.Core.Controls;
using Canvas_CSharp.Core.Turtle;
using Canvas_CSharp.Core.Utility;
using static SDL2.SDL;
namespace Canvas_CSharp.Core.Renderer;

public readonly struct Window
{
    public string Title { get; }
    public int ViewWidth { get; }
    public int ViewHeight { get; }
    internal SDL_WindowFlags WindowFlags { get; }
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
    
    /// <summary>
    /// Runs an interactive canvas.
    /// </summary>
    /// <param name="state">A state which will be changed throughout the program.</param>
    /// <param name="draw">A function to determine how to draw the frame.</param>
    /// <param name="onKeyPressed">A function to determine how to react when a key is pressed.</param>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public void RunApp<TState>(TState state, Func<Canvas, TState, Canvas> draw, Func<TState, Key, Option<TState>> onKeyPressed) where TState : notnull
    {
        Renderer.RunApp(Title, ViewWidth, ViewHeight, state, draw, onKeyPressed);
    }
    
    /// <summary>
    /// Runs a stateless non-interactive app. Does not render more than one frame.
    /// </summary>
    /// <param name="draw">A function to determine how to draw the frame.</param>
    public void RunSimpleApp(Func<Canvas, Canvas> draw)
    {
        Renderer.RunApp(Title, ViewWidth, ViewHeight, (byte)0, (canvas, _) => draw(canvas), (_, _) => new Option<byte>().None());
    }

    /// <summary>
    /// Runs a stateless non-interactive app where the image is drawn by turtle. Does not render more than one frame.
    /// </summary>
    /// <param name="turtle">A turtle to determine how to draw the frame.</param>
    public void TurtleDraw(Turtle.Turtle turtle)
    {
        var draw = TurtleInterpreter.Interpret(turtle);
        Renderer.RunApp(Title, ViewWidth, ViewHeight, (byte)0, (canvas, _) => draw(canvas), (_, _) => new Option<byte>().None());

    }
}