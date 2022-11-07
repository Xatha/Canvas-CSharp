using System.Drawing;
using SDL2;
using static SDL2.SDL;

namespace Canvas_CSharp.Core.Renderer;

public static class Drawer
{
    public static void SetFillBox(in Canvas canvas, in Color color, in Point start, in Point end)
    {
        int x = start.X, y = start.Y;
        int h = end.Y - start.Y, w = end.X - start.X;
        
        var rectangle = new SDL_Rect { x = x, y = y, h = h, w = w};
        SDL_SetRenderDrawColor(canvas.Renderer.SdlRenderer, color.R, color.G, color.B, color.A);
        SDL_RenderFillRect(canvas.Renderer.SdlRenderer, ref rectangle);
    }

    public static void SetLine(in Canvas canvas, in Color color, in Point start, in Point end)
    {
        int x1 = start.X, y1 = start.Y;
        int x2 = end.X, y2 = end.Y;

        int diffX = Math.Abs(x2 - x1), diffY = Math.Abs(y2 - y1);
        var mid = Math.Max(diffX, diffY);

        if (mid == 0) return;
        
        for (int i = 0; i < mid; i++)
        {
            var x = ((mid - i) * x1 + i * x2) / mid; 
            var y = ((mid - i) * y1 + i * y2) / mid;
            SetPixel(canvas, color, x, y);
        }
    }

    public static void SetPixel(in Canvas canvas, in Color color, in int x, in int y)
    {
        if (x < 0 || y < 0 || x >= canvas.ViewWidth || y >= canvas.ViewHeight) return;
        SDL_SetRenderDrawColor(canvas.Renderer.SdlRenderer, color.R, color.G, color.B, color.A);
        SDL_RenderDrawPoint(canvas.Renderer.SdlRenderer, x, y);
    }
}