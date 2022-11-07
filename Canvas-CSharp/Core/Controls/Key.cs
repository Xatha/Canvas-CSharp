using SDL2;

namespace Canvas_CSharp.Core.Controls;

public struct Key
{
    public SDL.SDL_Keycode Value { get; }
    public Key(SDL.SDL_Keycode keycode)
    {
        Value = keycode;
    }
}
