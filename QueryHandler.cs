using Microsoft.Extensions.AI;
using OpenAI;

namespace shell_chat;

/// <summary>
/// Handles LLM query processing.
/// </summary>
public class QueryHandler : IQueryHandler
{
    private const string DefaultModel = "gpt-4o-mini";
    private const string AdvancedModel = "gpt-5-mini";

    /// <inheritdoc />
    public async Task ProcessQueryAsync(string? query, string? apiKey = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(query))
        {
            Console.WriteLine("No query provided. Use --help for more information.");
            return;
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            ConsoleHelper.DisplayApiKeyNotConfiguredError();
            return;
        }

        try
        {
            IChatClient chatClient = new OpenAIClient(apiKey)
                .GetChatClient(DefaultModel)
                .AsIChatClient();

            var response = await chatClient.GetResponseAsync(query, cancellationToken: cancellationToken);

            Console.WriteLine(response.Text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error communicating with OpenAI: {ex.Message}");
        }
    }
}
