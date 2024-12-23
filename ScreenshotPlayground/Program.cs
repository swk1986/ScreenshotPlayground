using System.Diagnostics;
using System.Drawing.Imaging;
using ScreenshotPlayground;

var windowHandle =
    Process.GetProcesses().FirstOrDefault(p =>
        p.MainWindowTitle.Contains("Hevos - Abrechnung"))?.MainWindowHandle
    ?? throw new Exception("window not found");


System.Threading.Thread.Sleep(2500);

var image = Screenshot.FromWindow(windowHandle)
    .IncludeFrame()
    .Take();
image.Save("screenshot.png", ImageFormat.Png);

// Screenshot eines Bereichs innerhalb des Fensters erstellen (ohne Rahmen)
var areaImage = Screenshot.FromWindow(windowHandle)
    .ExcludeFrame()
    .CaptureArea(100, 100, 200, 150)
    .Take();
areaImage.Save("area_screenshot.png", ImageFormat.Png);