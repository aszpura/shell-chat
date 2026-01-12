using System.CommandLine;

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
    /// Processes the message action when the command is invoked.
    /// </summary>
    /// <param name="parseResult">The parse result containing command-line arguments.</param>
    /// <returns>Exit code: 0 for success.</returns>
    public static int HandleMessageCommand(ParseResult parseResult)
    {
        var message = parseResult.GetValue(MessageOption);
        var handler = new MessageHandler();
        handler.ProcessMessage(message);
        return 0;
    }
}
