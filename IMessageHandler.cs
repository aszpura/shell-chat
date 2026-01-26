namespace shell_chat;

/// <summary>
/// Defines the contract for handling messages.
/// </summary>
public interface IMessageHandler
{
    /// <summary>
    /// Processes the given message with optional API key.
    /// </summary>
    /// <param name="message">The message to process.</param>
    /// <param name="apiKey">Optional API key for LLM communication.</param>
    void ProcessMessage(string? message, string? apiKey = null);
}
