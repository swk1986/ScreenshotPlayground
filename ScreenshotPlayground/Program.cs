using System.Diagnostics;
using System.Drawing.Imaging;
using ScreenshotPlayground;

var windowHandle =
    Process.GetProcesses().FirstOrDefault(p =>
        p.MainWindowTitle.Contains("Hevos - Abrechnung"))?.MainWindowHandle
    ?? throw new Exception("window not found");

// windowHandle = new IntPtr(0x00000000000F0BD6);
//System.Threading.Thread.Sleep(2500);

if (!NativeMethods.IsIconic(windowHandle))
{
    Screenshot.FromWindow(windowHandle)
        .ExcludeFrame()
        .Take()
        .Save("screenshot_ExcludeFrame1.png", ImageFormat.Png);
    
   Screenshot.FromWindow(windowHandle)
        .IncludeFrame()
        .Take()
        .Save("screenshot_IncludeFrame1.png", ImageFormat.Png);
}
