namespace shell_chat;

public class MessageHandler
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
            Console.WriteLine("Error: No API key configured.");
            Console.WriteLine();
            Console.WriteLine("Configure an API key using one of these methods (in priority order):");
            Console.WriteLine("  1. Command-line:  shc --api-key YOUR_KEY -m \"message\"");
            Console.WriteLine("  2. Environment:   set SHELLCHAT_API_KEY=YOUR_KEY");
            Console.WriteLine("  3. Config file:   shc config set-key YOUR_KEY");
            Console.WriteLine();
            Console.WriteLine("Run 'shc config show' to see current configuration.");
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
