using System.Drawing;
using Canvas_CSharp.Core.Controls;
using Canvas_CSharp.Core.Renderer;
using Canvas_CSharp.Core.Utility;
using SDL2;
using Color = Canvas_CSharp.Core.Renderer.Color;

var window = new Window("Name", 500, 500);
window.RunApp(State.RedStart, Draw, OnKeyPressed);

Canvas Draw(Canvas canvas, State state)
{
    var width = canvas.ViewWidth;
    var height = canvas.ViewHeight;
    var halfWidth = width / 2;
    var halfHeight = height / 2;

    var (c1, c2, c3, c4) = GetPalette(state);
    Drawer.SetFillBox(canvas, c1, new Point(0, 0), new Point(halfWidth, halfHeight));
    Drawer.SetFillBox(canvas, c2, new Point(0, halfHeight), new Point(halfWidth, height));
    Drawer.SetFillBox(canvas, c3, new Point(halfWidth, halfHeight), new Point(width, height));
    Drawer.SetFillBox(canvas, c4, new Point(halfWidth, 0), new Point(width, halfHeight));
    return canvas;
}

Option<State> OnKeyPressed(State state, Key key)
{
    var option = new Option<State>();
    return key.Value switch
    {
        SDL.SDL_Keycode.SDLK_RIGHT => option.Some(CycleState(state)),
        SDL.SDL_Keycode.SDLK_LEFT => option.Some(CycleStateBackwards(state)),
        _ => option.None()
    };
}

State CycleState(State state)
{
    return state switch
    {
        State.RedStart => State.GreenStart,
        State.GreenStart => State.BlueStart,
        State.BlueStart => State.YellowStart,
        State.YellowStart => State.RedStart,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
    };
}

State CycleStateBackwards(State state)
{
    return state switch
    {
        State.RedStart => State.YellowStart,
        State.YellowStart => State.BlueStart,
        State.BlueStart => State.GreenStart,
        State.GreenStart => State.RedStart,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
    };
}

(Color, Color, Color, Color) GetPalette(State state)
{
    return state switch
    {
        State.RedStart => (ColorPicker.Red, ColorPicker.Green, ColorPicker.Blue, ColorPicker.Yellow),
        State.GreenStart => (ColorPicker.Green, ColorPicker.Blue, ColorPicker.Yellow, ColorPicker.Red),
        State.BlueStart => (ColorPicker.Blue, ColorPicker.Yellow, ColorPicker.Red, ColorPicker.Green),
        State.YellowStart => (ColorPicker.Yellow, ColorPicker.Red, ColorPicker.Green, ColorPicker.Blue),
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
    };
}

public enum State
{
    RedStart,
    GreenStart,
    BlueStart,
    YellowStart
}
