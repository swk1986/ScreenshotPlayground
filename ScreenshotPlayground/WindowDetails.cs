namespace ScreenshotPlayground;
using System.Windows.Automation;

public class WindowDetails
{
    public static ControlType GetControlType(IntPtr hWnd)
    {
        return AutomationElement.FromHandle(hWnd).Current.ControlType;
    }

    public static bool IsWindow(IntPtr hWnd) => Equals(GetControlType(hWnd), ControlType.Window);
}