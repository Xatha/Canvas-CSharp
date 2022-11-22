using static SDL2.SDL;
using static SDL2.SDL.SDL_Keycode;

namespace Canvas_CSharp.Core.Controls;

public struct Key
{
    public SDL_Keycode Value { get; }

    public Key(SDL_Keycode keycode)
    {
        Value = keycode;
    }
}

/// <summary>
///     Keys that can be pressed on the keyboard. Often used in onKeyPressed functions.
/// </summary>
public enum Keys
{
    A = SDLK_a,
    B = SDLK_b,
    C = SDLK_c,
    D = SDLK_d,
    E = SDLK_e,
    F = SDLK_f,
    G = SDLK_g,
    H = SDLK_h,
    I = SDLK_i,
    J = SDLK_j,
    K = SDLK_k,
    L = SDLK_l,
    M = SDLK_m,
    N = SDLK_n,
    O = SDLK_o,
    P = SDLK_p,
    Q = SDLK_q,
    R = SDLK_r,
    S = SDLK_s,
    T = SDLK_t,
    U = SDLK_u,
    V = SDLK_v,
    W = SDLK_w,
    X = SDLK_x,
    Y = SDLK_y,
    Z = SDLK_z,
    Num0 = SDLK_0,
    Num1 = SDLK_1,
    Num2 = SDLK_2,
    Num3 = SDLK_3,
    Num4 = SDLK_4,
    Num5 = SDLK_5,
    Num6 = SDLK_6,
    Num7 = SDLK_7,
    Num8 = SDLK_8,
    Num9 = SDLK_9,
    UpArrow = SDLK_UP,
    LeftArrow = SDLK_LEFT,
    RightArrow = SDLK_RIGHT,
    DownArrow = SDLK_DOWN,
    Space = SDLK_SPACE,
    Escape = SDLK_ESCAPE
}