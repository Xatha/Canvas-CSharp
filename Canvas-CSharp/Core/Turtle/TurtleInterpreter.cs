using System.Drawing;
using Canvas_CSharp.Core.Renderer;

namespace Canvas_CSharp.Core.Turtle;

internal struct TurtleInterpreter
{
    internal static Func<Canvas, Canvas> Interpret(Turtle turtle)
    {
        return canvas =>
        {
            var currentColor = ColorPicker.Black;
            var currentPos = new Point(canvas.ViewWidth / 2, canvas.ViewHeight / 2);
            var currentAngleDegrees = 90.0;
            var isPenUp = false;

            foreach (var command in turtle.TurtleCommandsList)
            {
                switch (command.CommandType)
                {
                    case TurtleCommandType.PenUp:
                        isPenUp = command.PenState!.Value;
                        break;
                    case TurtleCommandType.PenDown:
                        isPenUp = command.PenState!.Value;
                        break;
                    case TurtleCommandType.Move:
                        if (!isPenUp)
                        {
                            var newPos = GetNewPosition(currentPos, currentAngleDegrees, command.MoveDistance!.Value);
                            Drawer.SetLine(canvas, currentColor, currentPos, newPos);
                            currentPos = newPos;
                        }

                        break;
                    case TurtleCommandType.Turn:
                        currentAngleDegrees += command.TurnAngle!.Value;
                        break;
                    case TurtleCommandType.SetColor:
                        currentColor = command.Color!.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(command.CommandType.ToString());
                }
            }

            return canvas;
        };
    }
    
    private static Point GetNewPosition(Point pos, double angleDeg, int distance)
    {
        var x = pos.X + distance * Math.Cos(angleDeg * (Math.PI / 180));
        var y = pos.Y - distance * Math.Sin(angleDeg * (Math.PI / 180));
        return new Point((int)x, (int)y);
    }
}
