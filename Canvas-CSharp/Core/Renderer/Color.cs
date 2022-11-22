namespace Canvas_CSharp.Core.Renderer;

public readonly struct Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public byte A { get; }

    public Color(byte r, byte g, byte b, byte a)
    {
        A = a;
        B = b;
        G = g;
        R = r;
    }

    public int ConvertToInt()
    {
        return (A << 24) | (B << 16) | (G << 8) | R;
    }

    public static Color ConvertFromInt(int color)
    {
        return new Color((byte)(color & 0xFF), (byte)((color >> 8) & 0xFF), (byte)((color >> 16) & 0xFF),
            (byte)((color >> 24) & 0xFF));
    }
}