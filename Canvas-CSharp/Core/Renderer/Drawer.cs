using System.Drawing;
using static SDL2.SDL;

namespace Canvas_CSharp.Core.Renderer;

public static class Drawer
{
    /// <summary>
    ///     Draws a pixel on the screen.
    /// </summary>
    /// <param name="canvas">The canvas to draw the pixel onto.</param>
    /// <param name="color">The color of the pixel.</param>
    /// <param name="position">The position of the pixel.</param>
    public static void SetPixel(in Canvas canvas, in Color color, in Point position)
    {
        SDL_SetRenderDrawColor(canvas.Renderer.SdlRenderer, color.R, color.G, color.B, color.A);
        SDL_RenderDrawPoint(canvas.Renderer.SdlRenderer, position.X, position.Y);
    }

    /// <summary>
    ///     Draw a line from point A to point B
    /// </summary>
    /// <param name="canvas">The canvas to draw the line onto.</param>
    /// <param name="color">The color of the line.</param>
    /// <param name="start">Point A.</param>
    /// <param name="end">Point B.</param>
    public static void SetLine(in Canvas canvas, in Color color, in Point start, in Point end)
    {
        SDL_SetRenderDrawColor(canvas.Renderer.SdlRenderer, color.R, color.G, color.B, color.A);
        SDL_RenderDrawLine(canvas.Renderer.SdlRenderer, start.X, start.Y, end.X, end.Y);
    }

    /// <summary>
    ///     Draws a box onto the canvas.
    /// </summary>
    /// <param name="canvas">The canvas to draw the box onto.</param>
    /// <param name="color">The color of the box.</param>
    /// <param name="start">The top-left corner of the box.</param>
    /// <param name="end">The bottom-right corner of the box.</param>
    public static void SetBox(in Canvas canvas, in Color color, in Point start, in Point end)
    {
        var rectangle = new SDL_Rect
        {
            x = start.X,
            y = start.Y,
            h = end.Y - start.Y,
            w = end.X - start.X
        };
        SDL_SetRenderDrawColor(canvas.Renderer.SdlRenderer, color.R, color.G, color.B, color.A);
        SDL_RenderDrawRect(canvas.Renderer.SdlRenderer, ref rectangle);
    }

    /// <summary>
    ///     Draws a box with a filled color onto the canvas.
    /// </summary>
    /// <param name="canvas">The canvas to draw the box onto.</param>
    /// <param name="color">The color of the box.</param>
    /// <param name="start">The top-left corner of the box.</param>
    /// <param name="end">The bottom-right corner of the box.</param>
    public static void SetFillBox(in Canvas canvas, in Color color, in Point start, in Point end)
    {
        var rectangle = new SDL_Rect
        {
            x = start.X,
            y = start.Y,
            h = end.Y - start.Y,
            w = end.X - start.X
        };
        SDL_SetRenderDrawColor(canvas.Renderer.SdlRenderer, color.R, color.G, color.B, color.A);
        SDL_RenderFillRect(canvas.Renderer.SdlRenderer, ref rectangle);
    }

    /// <summary>
    ///     Get the color of a pixel at a given position.
    /// </summary>
    /// <param name="canvas">The canvas where pixel resides.</param>
    /// <param name="pixelPosition">The position of the pixel.</param>
    /// <remarks>WARNING: This operation is slow. This should not be used often.</remarks>
    /// <returns></returns>
    public static Color GetPixelColor(in Canvas canvas, in Point pixelPosition)
    {
        var rectangle = new SDL_Rect
        {
            x = 0,
            y = 0,
            h = canvas.ViewHeight,
            w = canvas.ViewWidth
        };

        unsafe
        {
            var surface = (SDL_Surface*)SDL_CreateRGBSurfaceWithFormat(0, canvas.ViewWidth, canvas.ViewHeight, 32,
                SDL_PIXELFORMAT_ABGR8888);
            var format = (SDL_PixelFormat*)surface->format;
            SDL_RenderReadPixels(canvas.Renderer.SdlRenderer, ref rectangle, SDL_PIXELFORMAT_ABGR8888, surface->pixels,
                surface->pitch);

            var targetPixel = (uint*)((byte*)surface->pixels
                                      + pixelPosition.Y * surface->pitch
                                      + pixelPosition.X * format->BytesPerPixel);
            SDL_GetRGBA(*targetPixel, surface->format, out var r, out var g, out var b, out var a);
            SDL_FreeSurface((IntPtr)surface);
            return new Color(r, g, b, a);
        }
    }
}