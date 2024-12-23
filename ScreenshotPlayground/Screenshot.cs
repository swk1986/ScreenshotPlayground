using System.Drawing;
using System.Runtime.InteropServices;

namespace ScreenshotPlayground;

public static class Screenshot
{
    public enum TernaryRasterOperations : uint
    {
        SRCCOPY = 0x00CC0020,
        SRCPAINT = 0x00EE0086,
        SRCAND = 0x008800C6,
        SRCINVERT = 0x00660046,
        SRCERASE = 0x00440328,
        NOTSRCCOPY = 0x00330008,
        NOTSRCERASE = 0x001100A6,
        MERGECOPY = 0x00C000CA,
        MERGEPAINT = 0x00BB0226,
        PATCOPY = 0x00F00021,
        PATPAINT = 0x00FB0A09,
        PATINVERT = 0x005A0049,
        DSTINVERT = 0x00550009,
        BLACKNESS = 0x00000042,
        WHITENESS = 0x00FF0062
    }

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
        int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

    [DllImport("user32.dll")]
    public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlags);

    [DllImport("user32.dll")]
    private static extern uint GetDpiForWindow(IntPtr hwnd);

    public static ScreenshotBuilder FromWindow(IntPtr hwnd)
    {
        return new ScreenshotBuilder(hwnd);
    }

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public class ScreenshotBuilder
    {
        private readonly IntPtr hwnd;
        private bool includeFrame = true;
        private int x, y, width, height;

        public ScreenshotBuilder(IntPtr hwnd)
        {
            this.hwnd = hwnd;
        }

        public ScreenshotBuilder IncludeFrame()
        {
            includeFrame = true;
            return this;
        }

        public ScreenshotBuilder ExcludeFrame()
        {
            includeFrame = false;
            return this;
        }

        public ScreenshotBuilder CaptureArea(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            return this;
        }

        public Bitmap Take()
        {
            var dpi = GetDpiForWindow(hwnd);
            var scaleFactor = dpi / 96f;

            if (x != 0 || y != 0 || width != 0 || height != 0)
            {
                // Bereich erfassen
                var fullScreenshot = CaptureWindow(hwnd, includeFrame, scaleFactor);

                // Koordinaten und Größe mit DPI-Skalierung anpassen
                x = (int)(x * scaleFactor);
                y = (int)(y * scaleFactor);
                width = (int)(width * scaleFactor);
                height = (int)(height * scaleFactor);

                // Screenshot auf den gewünschten Bereich zuschneiden
                var cropRect = new Rectangle(x, y, width, height);
                var croppedScreenshot = fullScreenshot.Clone(cropRect, fullScreenshot.PixelFormat);

                return croppedScreenshot;
            }

            // Gesamtes Fenster erfassen
            return CaptureWindow(hwnd, includeFrame, scaleFactor);
        }

        private static Bitmap CaptureWindow(IntPtr hwnd, bool includeFrame, float scaleFactor)
        {
            var rc = new RECT();
            GetWindowRect(hwnd, ref rc);

            var width = (int)((rc.Right - rc.Left) * scaleFactor);
            var height = (int)((rc.Bottom - rc.Top) * scaleFactor);

            var hdcSrc = GetDC(hwnd);
            var hdcDest = CreateCompatibleDC(hdcSrc);
            var hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
            var hOld = SelectObject(hdcDest, hBitmap);

            // Fensterrahmen ein- oder ausschließen
            var nFlags = includeFrame ? 1 : 0;
            PrintWindow(hwnd, hdcDest, nFlags);

            SelectObject(hdcDest, hOld);
            DeleteDC(hdcDest);
            ReleaseDC(hwnd, hdcSrc);

            var bmp = Image.FromHbitmap(hBitmap);
            DeleteObject(hBitmap);

            return bmp;
        }
    }
}