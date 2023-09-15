
using TestImageCompression;

var testPngBytes = File.ReadAllBytes("./test.png");

H.Log("Original", testPngBytes.Length);

var tests = new[]
{
    100,
    98,
    95,
    90,
    85,
    80,
    75,
};

foreach (var test in tests)
{
    await WebpBenchmarks.Compress(testPngBytes, test, 8000);
    await WebpBenchmarks.Compress(testPngBytes, test, 1200);
}

// Used for building unit tests in FS
// WebpBenchmarks.GetBytes(testPngBytes);
