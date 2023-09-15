using System.Diagnostics;
using ImageMagick;
using ImageMagick.Formats;

namespace TestImageCompression;

public static class WebpBenchmarks
{
    public static async Task Compress(byte[] testPngBytes, int quality, int maxHeight)
    {
        var startTime = Stopwatch.GetTimestamp();

        using var inStream = new MemoryStream(testPngBytes);
        using var image = new MagickImage(inStream);

        image.Strip();

        var fileName = $"out.{quality}";

        var defines = new WebPWriteDefines
        {
            Lossless = true,
            Method = 6,
        };

        if (quality < 100)
        {
            image.Quality = quality;
            defines.Lossless = false;
            defines.Method = 5;
        }

        if (image.Height > maxHeight)
        {
            image.Resize(0, maxHeight);
            fileName += $".{maxHeight}";
        }

        fileName += ".webp";

        await image.WriteAsync(fileName, defines);

        var elapsed = Stopwatch.GetElapsedTime(startTime);
        H.Log(fileName, new FileInfo(fileName).Length, elapsed);
    }

    public static async Task GetBytesAsBase64(byte[] testPngBytes)
    {
        using var inStream = new MemoryStream(testPngBytes);
        using var image = new MagickImage(inStream);

        image.Strip();

        using var ms = new MemoryStream();

        await image.WriteAsync(ms);

        var array = ms.ToArray();

        Console.WriteLine(Convert.ToBase64String(array));
    }
}
