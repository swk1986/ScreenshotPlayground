using System.Drawing;

namespace ScreenshotPlayground;

internal static class BitmapExtensions
{
    public static Bitmap Crop(
        this Bitmap bmp,
        Func<Size, Rectangle> sizeResolver)
    {
        var region = sizeResolver.Invoke(bmp.Size);
        return Crop(bmp, region);
    }

    public static Bitmap Crop(this Bitmap bmp, Rectangle region)
    {
        var cropped = bmp.Clone(region, bmp.PixelFormat);
        return cropped;
    }
}