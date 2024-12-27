using System.Diagnostics;
using System.Drawing.Imaging;
using ScreenshotPlayground;

var windowHandle =
    Process.GetProcesses().Single(p =>
        p.MainWindowTitle.Contains("Lineal"))?.MainWindowHandle
    ?? throw new Exception("window not found");

windowHandle = new IntPtr(0x00000000000209EC);
WindowToBmp.FromAnyWindow(windowHandle)?.Save($"FromAnyHandle_{windowHandle}.png", ImageFormat.Png);
WindowToBmp.FromVisibleWindow(windowHandle)?.Save($"FromVisibleWindow_{windowHandle}.png", ImageFormat.Png);
WindowToBmp.FromWindow(windowHandle)?.Save($"FromWindow_{windowHandle}.png", ImageFormat.Png);
