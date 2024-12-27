using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lineal;

public static class NativeMethods
{
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
    
    [DllImport("user32.dll")]
    internal static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);
    
    [DllImport("user32.dll")]
    internal static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
    
    [DllImport("user32.dll")]
    internal static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
}