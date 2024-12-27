using System.Text;

namespace ScreenshotPlayground;

public static class ControlVisibilityHelper
{
    private const uint GwHwndprev = 3;

    public static bool IsWindowFullyVisible(IntPtr hWnd)
    {
        // 1. Überprüfen, ob das Fenster überhaupt sichtbar ist.
        if (!NativeMethods.IsWindowVisible(hWnd)) return false;

        // 2. Die Rechteckkoordinaten des Fensters abrufen.
        if (!NativeMethods.GetClientRect(hWnd, out var windowRect)) return false;
        NativeMethods.ClientToScreen(hWnd, out var p);
        windowRect = windowRect.Move(p.X, p.Y);


        // 3. Überprüfen, ob das Fenster von anderen Fenstern verdeckt wird.

        var hWndWindow = NativeMethods.GetAncestor(hWnd, NativeMethods.GetAncestorFlags.GetRoot);
        var hWndToCheck = NativeMethods.GetAncestor(hWnd, NativeMethods.GetAncestorFlags.GetRoot); // GetTopWindow(IntPtr.Zero);


        while (hWndToCheck != IntPtr.Zero)
        {
            Console.WriteLine(GetControlInfo(hWndToCheck));

            // Nur sichtbare Fenster überprüfen.
            if (NativeMethods.IsWindowVisible(hWndToCheck))
            {
                var windowRectToCheck = new NativeMethods.RECT();
                if (NativeMethods.GetWindowRect(hWndToCheck, ref windowRectToCheck) != IntPtr.Zero)
                    if (windowRectToCheck.IntersectsWith(windowRect)
                        && hWndToCheck != hWnd
                        && hWndToCheck != hWndWindow)
                        return false;
            }

            hWndToCheck = NativeMethods.GetWindow(hWndToCheck, GwHwndprev);
        }
        
        return true;
    }


    private static string GetControlInfo(IntPtr hWnd)
    {
        // Klasse des Controls ermitteln
        var className = new StringBuilder(256);
        NativeMethods.GetClassName(hWnd, className, className.Capacity);

        // Text des Controls ermitteln
        var windowText = new StringBuilder(256);
        NativeMethods.GetWindowText(hWnd, windowText, windowText.Capacity);

        return $"Klasse: {className}, Text: {windowText}";
    }
}