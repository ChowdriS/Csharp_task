public static class ProgressHelper
{
    public static async Task ShowProgressAsync(string message, int delay = 2000)
    {
        Console.Write(message);
        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(delay / 3);
            Console.Write(".");
        }
        Console.WriteLine();
    }
}
