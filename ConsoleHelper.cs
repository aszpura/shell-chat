namespace shell_chat;

/// <summary>
/// Provides helper methods for console output.
/// </summary>
public static class ConsoleHelper
{
    /// <summary>
    /// Displays an error message when no API key is configured.
    /// </summary>
    public static void DisplayApiKeyNotConfiguredError()
    {
        Console.WriteLine("Error: No API key configured.");
        Console.WriteLine();
        Console.WriteLine("Configure an API key using one of these methods (in priority order):");
        Console.WriteLine("  1. Command-line:  shc --api-key YOUR_KEY -q \"query\"");
        Console.WriteLine("  2. Environment:   set SHELLCHAT_API_KEY=YOUR_KEY");
        Console.WriteLine("  3. Config file:   shc config set-key YOUR_KEY");
        Console.WriteLine();
        Console.WriteLine("Run 'shc config show' to see current configuration.");
    }
}
