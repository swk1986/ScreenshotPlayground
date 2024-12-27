using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenshotPlayground;

internal static class NativeMethods
{
    [DllImport("user32.dll", ExactSpelling = true)]
    internal static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags gaFlags);

    [DllImport("user32.dll")]
    internal static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    internal static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

    [DllImport("user32.dll")]
    internal static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    internal static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll")]
    internal static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll")]
    internal static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
        int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

    [DllImport("user32.dll")]
    internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlags);

    [DllImport("user32.dll")]
    internal static extern uint GetDpiForWindow(IntPtr hwnd);


    [DllImport("gdi32.dll")]
    internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    internal static extern bool DeleteObject(IntPtr hObject);

    internal enum GetAncestorFlags
    {
        /// <summary>
        ///     Ruft das übergeordnete Fenster des angegebenen Fensters ab.
        /// </summary>
        GetParent = 1,

        /// <summary>
        ///     Ruft das oberste Fenster im Fensterhierarchiebaum ab, zu dem das angegebene Fenster gehört.
        /// </summary>
        GetRoot = 2,

        /// <summary>
        ///     Ruft das Handle des Besitzers des angegebenen Fensters ab.
        /// </summary>
        GetRootOwner = 3
    }

    internal enum TernaryRasterOperations : uint
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


    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay(
        "Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}, Width: {Right - Left}, Height: {Bottom - Top}")]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    // [DllImport("gdi32.dll")]
    // internal static extern int SetMapMode(IntPtr hdc, int fnMapMode);
    //
    // [DllImport("gdi32.dll")]
    // internal static extern bool SetWindowExtEx(IntPtr hdc, int nXExtent, int nYExtent, IntPtr lpSize);
    //
    // [DllImport("gdi32.dll")]
    // internal static extern bool SetViewportExtEx(IntPtr hdc, int nXExtent, int nYExtent, IntPtr lpSize);

    // internal const int MM_ANISOTROPIC = 8;
    
    [DllImport("user32.dll")]
    internal static extern bool ClientToScreen(IntPtr hWnd, out Point lpPoint);
    
    // [DllImport("user32.dll")]
    // internal static extern bool EnumWindows(EnumWindowsProc enumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    internal  delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    
    
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    internal static bool DoesMethodExist(string module, string procName)
    {
        var libPtr = LoadLibrary(module);
        if (libPtr == IntPtr.Zero) return false;

        var procAddr = GetProcAddress(libPtr, procName);
        return procAddr != IntPtr.Zero;
    }
    
    //ANSI-Encodung important
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string procName);

    //ANSI-Encodung important
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);


}