using System.CommandLine;
using shell_chat.Configuration;

namespace shell_chat;

/// <summary>
/// Provides command-line configuration including options and their associated actions.
/// </summary>
public static class CommandConfiguration
{
    /// <summary>
    /// Gets the message option for command-line input.
    /// </summary>
    public static Option<string> MessageOption { get; } = CreateMessageOption();

    /// <summary>
    /// Gets the API key option for command-line input.
    /// </summary>
    public static Option<string> ApiKeyOption { get; } = CreateApiKeyOption();

    /// <summary>
    /// Creates and configures the message option.
    /// </summary>
    /// <returns>A configured Option for message input.</returns>
    private static Option<string> CreateMessageOption()
    {
        var messageOption = new Option<string>("--message")
        {
            Description = "The message to process."
        };
        messageOption.Aliases.Add("-m");
        
        return messageOption;
    }

    /// <summary>
    /// Creates and configures the API key option.
    /// </summary>
    /// <returns>A configured Option for API key input.</returns>
    private static Option<string> CreateApiKeyOption()
    {
        var apiKeyOption = new Option<string>("--api-key")
        {
            Description = "The API key for LLM communication. Overrides environment variable and config file."
        };
        apiKeyOption.Aliases.Add("-k");
        
        return apiKeyOption;
    }

    /// <summary>
    /// Processes the message action when the command is invoked.
    /// </summary>
    /// <param name="parseResult">The parse result containing command-line arguments.</param>
    /// <returns>Exit code: 0 for success.</returns>
    public static int HandleMessageCommand(ParseResult parseResult)
    {
        var message = parseResult.GetValue(MessageOption);
        var commandLineApiKey = parseResult.GetValue(ApiKeyOption);
        
        var configManager = new ConfigurationManager();
        var apiKey = configManager.ResolveApiKey(commandLineApiKey);

        var handler = new MessageHandler();
        handler.ProcessMessage(message, apiKey);
        return 0;
    }
}
