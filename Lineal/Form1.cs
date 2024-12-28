namespace Lineal;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Resize += UpdateCoordinates;
        Move += UpdateCoordinates;
        UpdateCoordinates(this, EventArgs.Empty);
    }

    private void UpdateCoordinates(object? sender, EventArgs e)
    {
        var windowRect = new NativeMethods.RECT();
        NativeMethods.GetWindowRect(Handle, ref windowRect);

        var clientRect = new NativeMethods.RECT();
        NativeMethods.GetClientRect(Handle, ref clientRect);

        var clientPos = new Point();
        NativeMethods.ClientToScreen(Handle, ref clientPos);

        Text = $"Lineal - Window: " +
               $"Left: {windowRect.Left}, " +
               $"Top: {windowRect.Top}, " +
               $"Right: {windowRect.Right}, " +
               $"Bottom: {windowRect.Bottom}, " +
               $"Width: {windowRect.Right - windowRect.Left}, " +
               $"Height: {windowRect.Bottom - windowRect.Top})";

        ClientRectLabel.Text =
            "relative Client: " +
            $"Left: {clientRect.Left}, " +
            $"Top: {clientRect.Top}, " +
            $"Right: {clientRect.Right}, " +
            $"Bottom: {clientRect.Bottom}, " +
            $"Width: {clientRect.Right - clientRect.Left}, " +
            $"Height: {clientRect.Bottom - clientRect.Top})" + 
            Environment.NewLine + 
            Environment.NewLine+
            "absolute Client: " +
            $"Left: {clientPos.X + clientRect.Left}, " +
            $"Top: {clientPos.Y + clientRect.Top}, " +
            $"Right: {clientPos.X + clientRect.Right}, " +
            $"Bottom: {clientPos.Y + clientRect.Bottom}, " +
            $"Width: {clientRect.Right - clientRect.Left}, " +
            $"Height: {clientRect.Bottom - clientRect.Top})";
    }
}