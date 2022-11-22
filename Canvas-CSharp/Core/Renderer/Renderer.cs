using System.Drawing;
using Canvas_CSharp.Core.Controls;
using Canvas_CSharp.Core.Utility;
using static SDL2.SDL;

namespace Canvas_CSharp.Core.Renderer;

internal sealed class Renderer
{
    private readonly int _viewHeight;
    private readonly int _viewWidth;
    private readonly SDL_WindowFlags _windowFlags;


    private IntPtr _texture;


    internal Renderer(Window window)
    {
        _viewWidth = window.ViewWidth;
        _viewHeight = window.ViewHeight;
        _windowFlags = window.WindowFlags;
    }

    internal bool Persistent { get; init; } = false;
    internal IntPtr SdlRenderer { get; private set; }
    internal IntPtr SdlWindow { get; private set; }

    internal void RunApp<TState>(string title, int viewWidth, int viewHeight, TState state,
        Func<Canvas, TState, Canvas> draw,
        Func<TState, Key, Option<TState>> onKeyPressed) where TState : notnull
    {
        SDL_Init(SDL_INIT_VIDEO);

        SdlWindow = SDL_CreateWindow(title, 50, 50, viewWidth, viewHeight, _windowFlags);
        SDL_SetWindowTitle(SdlWindow, title);

        SdlRenderer = SDL_CreateRenderer(SdlWindow, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        _texture = SDL_CreateTexture(SdlRenderer, SDL_PIXELFORMAT_ABGR8888, 1, viewWidth, viewHeight);

        var shouldDraw = true;
        while (true)
        {
            if (shouldDraw)
            {
#if DEBUG
                Console.de("Draw!");
                var start = SDL_GetTicks();
                
                Draw(draw, state);
                
                var end = SDL_GetTicks();
                var time = end - start;
                Console.WriteLine($"Frame took {time} ms.");
#else

                Draw(draw, state);

#endif
                shouldDraw = false;
            }

            SDL_WaitEvent(out var sdlEvent);

            if (sdlEvent.type == SDL_EventType.SDL_QUIT) break;

            if (sdlEvent.type == SDL_EventType.SDL_KEYDOWN)
            {
                var sdlKey = sdlEvent.key.keysym.sym;
                var eOnKeyPressed = onKeyPressed(state, new Key(sdlKey));

                switch (eOnKeyPressed.Some(out var value))
                {
                    case true:
                        shouldDraw = true;
                        state = value!;
                        break;
                    case false:
                        shouldDraw = false;
                        break;
                }
            }
        }

        if (!Persistent) Destroy();
    }

    internal void Destroy()
    {
        SDL_DestroyTexture(_texture);
        SDL_DestroyRenderer(SdlRenderer);
        SDL_DestroyWindow(SdlWindow);
        SDL_Quit();
    }

    internal void SaveImage(string path)
    {
        if (Path.GetExtension(path) != ".bmp")
            throw new ArgumentException("File extension must be .bmp");
        if (File.Exists(path))
            throw new ArgumentException("File already exists, please choose a filename that does not already exist.");
        
        if (SDL_QueryTexture(_texture, out _, out _, out var w, out var h) != 0)
            throw new Exception(
                $"Failed to save the image. Could not query the texture, perhaps you called {nameof(SaveImage)} after the window was closed?");
        unsafe
        {
            var surface = (SDL_Surface*)SDL_CreateRGBSurface(0, w, h, 32, 0, 0, 0, 0);
            var format = (SDL_PixelFormat*)surface->format;
            var rect = new SDL_Rect { x = 0, y = 0, w = w, h = h };

            SDL_RenderReadPixels(SdlRenderer, ref rect, format->format, surface->pixels, surface->pitch);
            SDL_SaveBMP((IntPtr)surface, path);
        }
    }

    private void Draw<TState>(Func<Canvas, TState, Canvas> draw, TState state) where TState : notnull
    {
        var canvas = new Canvas(_viewWidth, _viewHeight, this);
        Drawer.SetFillBox(canvas, ColorPicker.White, new Point(0, 0), new Point(_viewWidth, _viewHeight));
        draw(canvas, state);
        SDL_RenderPresent(SdlRenderer);
    }
}