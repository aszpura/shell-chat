namespace shell_chat;

/// <summary>
/// Handles LLM query processing.
/// </summary>
public class QueryHandler : IQueryHandler
{
    /// <inheritdoc />
    public void ProcessQuery(string? query, string? apiKey = null)
    {
        if (string.IsNullOrEmpty(query))
        {
            Console.WriteLine("No query provided. Use --help for more information.");
            return;
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("Error: No API key configured.");
            Console.WriteLine();
            Console.WriteLine("Configure an API key using one of these methods (in priority order):");
            Console.WriteLine("  1. Command-line:  shc --api-key YOUR_KEY -q \"query\"");
            Console.WriteLine("  2. Environment:   set SHELLCHAT_API_KEY=YOUR_KEY");
            Console.WriteLine("  3. Config file:   shc config set-key YOUR_KEY");
            Console.WriteLine();
            Console.WriteLine("Run 'shc config show' to see current configuration.");
            return;
        }

        // TODO: Replace with actual LLM call
        Console.WriteLine("Hello World!");
        Console.WriteLine($"Query: {query}");
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
