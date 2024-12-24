using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Automation;

namespace ScreenshotPlayground;

public static class MinWindowScreenshot
{
    public static Bitmap CaptureWindow(IntPtr hwnd)
    {
        // Warte kurz, um sicherzustellen, dass das Fenster vollständig geladen ist
        Thread.Sleep(100);

        // AutomationElement für das Fenster erstellen
        var windowElement = AutomationElement.FromHandle(hwnd);
        var allElements = windowElement.FindAll(TreeScope.Subtree, Condition.TrueCondition);
        // Rekursiv alle untergeordneten Elemente durchsuchen und ein Bitmap erstellen
        var screenshot = CaptureElement(windowElement);

        return screenshot;
    }

    private static Bitmap CaptureElement(AutomationElement element)
    {
        // Bitmap für den Screenshot erstellen
        var bmp = new Bitmap((int)element.Current.BoundingRectangle.Width,
            (int)element.Current.BoundingRectangle.Height, PixelFormat.Format32bppArgb);
        using (var graphics = Graphics.FromImage(bmp))
        {
            // Zeichne den Hintergrund des Elements
            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

            // Rekursiv alle untergeordneten Elemente durchsuchen und zeichnen
            CaptureChildElements(element, graphics);
        }

        return bmp;
    }

    private static void CaptureChildElements(AutomationElement element, Graphics graphics)
    {
        // Alle untergeordneten Elemente abrufen
        var childElements = element.FindAll(TreeScope.Children, Condition.TrueCondition);

        // Jedes untergeordnete Element durchlaufen
        foreach (AutomationElement childElement in childElements)
            // Wenn das Element sichtbar ist, seinen Inhalt zeichnen
            if (childElement.Current.IsOffscreen == false)
            {
                // Rechteck des Elements abrufen
                var rect = childElement.Current.BoundingRectangle;

                // Elementinhalt in das Bitmap zeichnen
                graphics.CopyFromScreen((int)rect.Left, (int)rect.Top,
                    (int)(rect.Left - element.Current.BoundingRectangle.Left),
                    (int)(rect.Top - element.Current.BoundingRectangle.Top),
                    new Size((int)rect.Width, (int)rect.Height),
                    CopyPixelOperation.SourceCopy);

             
            

                // Rekursiv die untergeordneten Elemente des aktuellen Elements durchsuchen
                CaptureChildElements(childElement, graphics);
                
                   
                Bitmap myBitmap = new Bitmap((int)rect.Width, (int)rect.Height, graphics);
                myBitmap.Save($"window_{childElement.Current.NativeWindowHandle}.png", ImageFormat.Png);
            }
    }
}
