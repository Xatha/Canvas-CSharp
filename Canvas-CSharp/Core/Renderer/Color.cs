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
}