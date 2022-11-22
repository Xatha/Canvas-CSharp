using Canvas_CSharp.Core.Renderer;

namespace Canvas_CSharp.Core.Turtle;

public readonly struct Turtle
{
    internal List<TurtleCommand> TurtleCommandsList { get; }

    /// <summary>
    ///     Initialises a new instance of the <see cref="Turtle" /> struct.
    ///     This struct uses a fluid interface to allow for chaining of commands.
    /// </summary>
    /// <remarks>The turtle starts at the origin facing up, with the pen down and colored black.</remarks>
    public Turtle()
    {
        TurtleCommandsList = new List<TurtleCommand>();
    }

    /// <summary>
    ///     Moves the turtle forward by the specified distance.
    /// </summary>
    public Turtle Move(int distance)
    {
        TurtleCommandsList.Add(new TurtleCommand(TurtleCommandType.Move, distance));
        return this;
    }

    /// <summary>
    ///     Turns the turtle forward by the specified degrees.
    /// </summary>
    public Turtle Turn(double degrees)
    {
        TurtleCommandsList.Add(new TurtleCommand(TurtleCommandType.Turn, degrees));
        return this;
    }

    /// <summary>
    ///     Tells the turtle to stop drawing.
    /// </summary>
    public Turtle PenUp()
    {
        TurtleCommandsList.Add(new TurtleCommand(TurtleCommandType.PenUp, true));
        return this;
    }

    /// <summary>
    ///     Tells the turtle to start drawing again.
    /// </summary>
    public Turtle PenDown()
    {
        TurtleCommandsList.Add(new TurtleCommand(TurtleCommandType.PenDown, false));
        return this;
    }

    /// <summary>
    ///     Sets the colour of the turtle's pen.
    /// </summary>
    public Turtle SetColor(Color color)
    {
        TurtleCommandsList.Add(new TurtleCommand(TurtleCommandType.SetColor, color));
        return this;
    }

    /// <summary>
    ///     Combine the instructions from a second turtle into this one.
    /// </summary>
    public Turtle Combine(Turtle turtle)
    {
        TurtleCommandsList.AddRange(turtle.TurtleCommandsList);
        return this;
    }
}