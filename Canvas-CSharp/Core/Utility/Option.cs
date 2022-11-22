using System.Diagnostics.CodeAnalysis;

namespace Canvas_CSharp.Core.Utility;

public struct Option<T> where T : notnull
{
    private T? _value;
    private bool _isSome;

    public bool Some([MaybeNullWhen(false)] out T value)
    {
        value = _value;
        return _isSome;
    }

    public Option<T> Some(T value)
    {
        _value = value;
        _isSome = true;
        return this;
    }

    public Option<T> None()
    {
        _isSome = false;
        return this;
    }
}