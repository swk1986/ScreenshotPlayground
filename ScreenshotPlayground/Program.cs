using System.Diagnostics;
using System.Drawing.Imaging;
using ScreenshotPlayground;

var windowHandle =
    Process.GetProcesses().FirstOrDefault(p =>
        p.MainWindowTitle.Contains("Hevos - Abrechnung"))?.MainWindowHandle
    ?? throw new Exception("window not found");


//System.Threading.Thread.Sleep(2500);

if (!Screenshot.IsIconic(windowHandle))
{
   // Screenshot.FromWindow(windowHandle)
   //      .IncludeFrame()
   //      .Take()
   //      .Save("screenshot_IncludeFrame.png", ImageFormat.Png);
   // Screenshot.FromWindow(windowHandle)
   //      .IncludeFrame()
   //      .Take()
   //      .Save("screenshot_IncludeFrame.png", ImageFormat.Png);


    Screenshot.FromWindow(windowHandle)
        .ExcludeFrame()
        .Take()
        .Save("screenshot_ExcludeFrame.png", ImageFormat.Png);

}
