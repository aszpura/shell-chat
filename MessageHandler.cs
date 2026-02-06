namespace shell_chat;

public class MessageHandler : IMessageHandler
{
    public void ProcessMessage(string? message, string? apiKey = null)
    {
        if (string.IsNullOrEmpty(message))
        {
            Console.WriteLine("No message provided. Use --help for more information.");
            return;
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            ConsoleHelper.DisplayApiKeyNotConfiguredError();
            return;
        }

        Console.WriteLine($"Processing message: {message}");
        Console.WriteLine($"API Key configured: Yes (using {MaskApiKey(apiKey)})");
    }

    /// <summary>
    /// Masks an API key for display, showing only the first 4 and last 4 characters.
    /// </summary>
    private static string MaskApiKey(string apiKey)
    {
        if (apiKey.Length <= 8)
        {
            return new string('*', apiKey.Length);
        }

        return $"{apiKey[..4]}...{apiKey[^4..]}";
    }
}
