using System.Drawing;

namespace ScreenshotPlayground;

public static class WindowToBmp
{
    private static float GetScaleFactor(IntPtr hWnd)
    {
        if (!NativeMethods.DoesMethodExist("user32.dll", "GetDpiForWindow"))
            return 1f;
        
        var dpi = NativeMethods.GetDpiForWindow(hWnd);
        var scaleFactor = dpi / 96f;
        return scaleFactor;
    }


    public static Bitmap? FromAnyWindow(IntPtr hWnd)
    {
        if (NativeMethods.IsIconic(hWnd))
            return null;

        var scaleFactor = GetScaleFactor(hWnd);
        NativeMethods.GetClientRect(hWnd, out var rc);

        var width = (int)((rc.Right - rc.Left) * scaleFactor);
        var height = (int)((rc.Bottom - rc.Top) * scaleFactor);

        var hdcSrc = NativeMethods.GetDC(hWnd);
        var hdcDest = NativeMethods.CreateCompatibleDC(hdcSrc);
        var hBitmap = NativeMethods.CreateCompatibleBitmap(hdcSrc, width, height);
        var hOld = NativeMethods.SelectObject(hdcDest, hBitmap);
        
        NativeMethods.PrintWindow(hWnd, hdcDest, 3);
        
        NativeMethods.SelectObject(hdcDest, hOld);
        NativeMethods.DeleteDC(hdcDest);
        NativeMethods.ReleaseDC(hWnd, hdcSrc);

        var bmp = Image.FromHbitmap(hBitmap);
        NativeMethods.DeleteObject(hBitmap);

        return bmp;
    }

    public static Bitmap? FromVisibleWindow(IntPtr hWnd)
    {
        if (NativeMethods.IsIconic(hWnd))
            return null;

        var scaleFactor = GetScaleFactor(hWnd);
        NativeMethods.GetClientRect(hWnd, out var rc);

        var width = (int)((rc.Right - rc.Left) * scaleFactor);
        var height = (int)((rc.Bottom - rc.Top) * scaleFactor);

        var hdcSrc = NativeMethods.GetDC(hWnd);
        if (hdcSrc == IntPtr.Zero)
            return null;

        var hdcDest = NativeMethods.CreateCompatibleDC(hdcSrc);
        if (hdcDest == IntPtr.Zero)
        {
            NativeMethods.ReleaseDC(hWnd, hdcSrc);
            return null;
            ;
        }

        var hBitmap = NativeMethods.CreateCompatibleBitmap(hdcSrc, width, height);
        if (hBitmap == IntPtr.Zero)
        {
            NativeMethods.DeleteDC(hdcDest);
            NativeMethods.ReleaseDC(hWnd, hdcSrc);
            return null;
            
        }

        var hOld = NativeMethods.SelectObject(hdcDest, hBitmap);

        NativeMethods.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0,
            NativeMethods.TernaryRasterOperations.SRCCOPY);

        NativeMethods.SelectObject(hdcDest, hOld);
        NativeMethods.DeleteDC(hdcDest);
        NativeMethods.ReleaseDC(hWnd, hdcSrc);

        try
        {
            var bmp = Image.FromHbitmap(hBitmap);
            return bmp;
        }
        finally
        {
            NativeMethods.DeleteObject(hBitmap);
        }
    }

    public static Bitmap? FromWindow(IntPtr hWnd)
    {
        Bitmap? bmp = null;
        var isFullyVisible = ControlVisibilityHelper.IsWindowFullyVisible(hWnd);
        Console.WriteLine($"isFullyVisible: {isFullyVisible}");
        
        if (isFullyVisible) bmp = FromVisibleWindow(hWnd);
        return bmp ?? FromAnyWindow(hWnd);
    }
}