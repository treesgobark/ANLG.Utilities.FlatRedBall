using FlatRedBall.Input;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class MouseExtensions
{
    public static IPressableInput GetPressableScrollWheel(this Mouse mouse, WheelDirection direction)
    {
        if (direction == WheelDirection.Up)
        {
            return new DelegateBasedPressableInput(
                () => mouse.ScrollWheelChange > 0,
                () => mouse.ScrollWheelChange > 0,
                () => mouse.ScrollWheelChange > 0
            );
        }
        else
        {
            return new DelegateBasedPressableInput(
                () => mouse.ScrollWheelChange < 0,
                () => mouse.ScrollWheelChange < 0,
                () => mouse.ScrollWheelChange < 0
            );
        }
    }

    public enum WheelDirection
    {
        Up,
        Down,
    }
}