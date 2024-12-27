namespace ScreenshotPlayground;

public static class RectExtensions
{
    internal static bool IntersectsWith(this NativeMethods.RECT rect1, NativeMethods.RECT rect2)
    {
        return rect1.Left < rect2.Right && rect1.Right > rect2.Left &&
               rect1.Top < rect2.Bottom && rect1.Bottom > rect2.Top;
    }

    internal static NativeMethods.RECT Move(this NativeMethods.RECT windowRect, int x, int y)
    {
        return new NativeMethods.RECT
        {
            Left = windowRect.Left + x,
            Top = windowRect.Top + y,
            Right = windowRect.Right + x,
            Bottom = windowRect.Bottom + y
        };
    }
}