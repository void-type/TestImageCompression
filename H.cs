namespace TestImageCompression
{
    public class H
    {
        public static void Log(string name, long size, TimeSpan? time = null)
        {
            var timeSpanString = time is not null ? $"{time}" : string.Empty;
            Console.WriteLine($"{name,-25}{size / 1024,10}KB in {timeSpanString}");
        }
    }
}
