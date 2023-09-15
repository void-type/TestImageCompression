using System.Diagnostics;
using ImageMagick;

namespace TestImageCompression;

public static class WebpBenchmarks
{
    public static async Task Compress(byte[] testPngBytes, int quality, int maxHeight)
    {
        var startTime = Stopwatch.GetTimestamp();

        using var inStream = new MemoryStream(testPngBytes);
        using var image = new MagickImage(inStream);

        image.Strip();

        if (quality >= 100)
        {
            image.Settings.SetDefine(MagickFormat.WebP, "lossless", true);
        }
        else
        {
            image.Quality = quality;
        }

        var fileName = $"out.{quality}";

        if (image.Height > maxHeight)
        {
            image.Resize(0, maxHeight);
            fileName += $".{maxHeight}";
        }

        fileName += ".webp";

        await image.WriteAsync(fileName);

        var elapsed = Stopwatch.GetElapsedTime(startTime);
        H.Log(fileName, new FileInfo(fileName).Length, elapsed);
    }
}
