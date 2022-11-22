using Canvas_CSharp.Core.Renderer;

namespace Canvas_CSharp.Core.Turtle;

internal readonly struct TurtleCommand
{
    internal readonly TurtleCommandType CommandType { get; }
    internal int? MoveDistance { get; } = null;
    internal double? TurnAngle { get; } = null;
    internal Color? Color { get; } = null;
    internal bool? PenState { get; } = null;

    internal TurtleCommand(TurtleCommandType commandType, int moveDistance)
    {
        CommandType = commandType;
        MoveDistance = moveDistance;
    }

    internal TurtleCommand(TurtleCommandType commandType, double turnAngleDegrees)
    {
        CommandType = commandType;
        TurnAngle = turnAngleDegrees;
    }

    internal TurtleCommand(TurtleCommandType commandType, Color color)
    {
        CommandType = commandType;
        Color = color;
    }

    internal TurtleCommand(TurtleCommandType commandType, bool penState)
    {
        CommandType = commandType;
        PenState = penState;
    }
}