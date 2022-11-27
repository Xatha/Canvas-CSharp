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


    /// <summary>
    ///     Creates a new window with the specified title, width, height. This will also create a renderer for the window.
    ///     Automatically cleans up all resources the user closes the window.
    /// </summary>
    public Window(string title, uint viewWidth, uint viewHeight)
    {
        Title = title;
        ViewWidth = (int)viewWidth;
        ViewHeight = (int)viewHeight;
        WindowFlags = SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS;
        Renderer = new Renderer(this);
    }

    /// <summary>
    ///     Creates a new window with the specified title, width, height, and flags. This will also create a renderer for the
    ///     window. Automatically cleans up all resources the user closes the window.
    /// </summary>
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
    ///     Creates a new window with the specified title, width, height, and flags. Allows for overwriting the renderer state.
    ///     If <paramref name="persistentRenderer" /> is set to true, the renderer will not be destroyed when the program
    ///     exits. This is useful if you want to use <see cref="SaveImage" /> after shutting down the window.
    ///     Always remember to call <see cref="Shutdown" /> when you are done using the renderer.
    ///     If you want to use the default renderer state, use the other constructor.
    /// </summary>
    /// <remarks>Always remember to call <see cref="Shutdown" /> after you're done with the program.</remarks>
    public Window(string title, uint viewWidth, uint viewHeight, bool persistentRenderer, SDL_WindowFlags windowFlags =
        SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS)
    {
        Title = title;
        ViewWidth = (int)viewWidth;
        ViewHeight = (int)viewHeight;
        WindowFlags = windowFlags;
        Renderer = new Renderer(this)
        {
            Persistent = persistentRenderer
        };
    }

    /// <summary>
    ///     Runs an interactive canvas.
    /// </summary>
    /// <param name="state">A state which will be changed throughout the program.</param>
    /// <param name="draw">A function to determine how to draw the frame.</param>
    /// <param name="onKeyPressed">A function to determine how to react when a key is pressed.</param>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public void RunApp<TState>(TState state, Func<Canvas, TState, Canvas> draw,
        Func<TState, Key, Option<TState>> onKeyPressed) where TState : notnull
    {
        Renderer.RunApp(Title, ViewWidth, ViewHeight, state, draw, onKeyPressed);
    }

    /// <summary>
    ///     Runs a stateless non-interactive app. Does not render more than one frame.
    /// </summary>
    /// <param name="draw">A function to determine how to draw the frame.</param>
    public void RunSimpleApp(Func<Canvas, Canvas> draw)
    {
        Renderer.RunApp(Title, ViewWidth, ViewHeight, (byte)0, (canvas, _) => draw(canvas),
            (_, _) => new Option<byte>().None());
    }

    public void RunAppWithTimer<TState>(TState state, Func<Canvas, TState, Canvas> draw,
        Func<TState, Key, Option<TState>> onKeyPressed) where TState : notnull
    {
        Renderer.RunAppWithTimer(Title, ViewHeight, ViewWidth, state, draw, onKeyPressed);
    }
    /// <summary>
    ///     Runs a stateless non-interactive app where the image is drawn by turtle. Does not render more than one frame.
    /// </summary>
    /// <param name="turtle">A turtle to determine how to draw the frame.</param>
    public void TurtleDraw(Turtle.Turtle turtle)
    {
        var draw = TurtleInterpreter.Interpret(turtle);
        Renderer.RunApp(Title, ViewWidth, ViewHeight, (byte)0, (canvas, _) => draw(canvas),
            (_, _) => new Option<byte>().None());
    }

    /// <summary>
    ///     Saves the current frame to a file.
    /// </summary>
    /// <param name="path">The path to the file you wanna save.</param>
    /// <remarks>
    ///     If you have to save the image after closing the window, you can set persistentRenderer to true in the
    ///     <see cref="Window" /> ctor.
    ///     This will make it so the renderer and window does not automatically get destroyed when quitting the window.
    ///     This is dangerous so remember to always call  <see cref="Shutdown" /> when you are done using the window.
    /// </remarks>
    public void SaveImage(string path)
    {
        Renderer.SaveImage(path);
    }

    /// <summary>
    ///     Manually destroys the renderer. This is not necessary if the renderer is not persistent.
    /// </summary>
    public void Shutdown()
    {
        Renderer.Destroy();
    }
}