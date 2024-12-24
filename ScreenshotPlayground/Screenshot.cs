using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ScreenshotPlayground;

public static class Screenshot
{

    public static ScreenshotBuilder FromWindow(IntPtr hwnd)
    {
        return new ScreenshotBuilder(hwnd);
    }


    public class ScreenshotBuilder(IntPtr hwnd)
    {
        private bool _includeFrame = true;

        public ScreenshotBuilder IncludeFrame()
        {
            _includeFrame = true;
            return this;
        }

        public ScreenshotBuilder ExcludeFrame()
        {
            _includeFrame = false;
            return this;
        }

        public Bitmap Take()
        {
            var dpi = NativeMethods.GetDpiForWindow(hwnd);
            var scaleFactor = dpi / 96f;
            return CaptureWindow(hwnd, _includeFrame, scaleFactor);
        }

        private static Bitmap CaptureWindow(IntPtr hwnd, bool includeFrame, float scaleFactor)
        {
            const int pwAll = 0;
            const int pwClientOnly = 1;

            var rc = new NativeMethods.RECT();
            NativeMethods.GetWindowRect(hwnd, ref rc);

            var clientRect = new NativeMethods.RECT();
            NativeMethods.GetClientRect(hwnd, ref clientRect);

            if (!includeFrame)
                rc = clientRect;

            var width = (int)((rc.Right - rc.Left) * scaleFactor);
            var height = (int)((rc.Bottom - rc.Top) * scaleFactor);

            var hdcSrc = NativeMethods.GetDC(hwnd);
            var hdcDest = NativeMethods.CreateCompatibleDC(hdcSrc);
            var hBitmap = NativeMethods.CreateCompatibleBitmap(hdcSrc, width, height);
            var hOld = NativeMethods.SelectObject(hdcDest, hBitmap);

            var nFlags = includeFrame ? pwAll : pwClientOnly;
            NativeMethods.PrintWindow(hwnd, hdcDest, nFlags);

            NativeMethods.SelectObject(hdcDest, hOld);
            NativeMethods.DeleteDC(hdcDest);
            NativeMethods.ReleaseDC(hwnd, hdcSrc);

            var bmp = Image.FromHbitmap(hBitmap);
            NativeMethods.DeleteObject(hBitmap);

            return bmp;
        }
    }
}