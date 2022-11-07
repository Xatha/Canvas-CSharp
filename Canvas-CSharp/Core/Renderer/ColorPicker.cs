namespace Canvas_CSharp.Core.Renderer;

public readonly struct ColorPicker
{
    public static Color White { get; } = new(255, 255, 255, 255);
    public static Color Black { get; } = new(0, 0, 0, 255);
    public static Color Red { get; } = new(255, 0, 0, 255);
    public static Color Green { get; } = new(0, 255, 0, 255);
    public static Color Blue { get; } = new(0, 0, 255, 255);
    public static Color Yellow { get; } = new(255, 255, 0, 255);
    public static Color Cyan { get; } = new(0, 255, 255, 255);
}