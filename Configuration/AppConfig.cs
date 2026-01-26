using System.Text.Json;

namespace shell_chat.Configuration;

/// <summary>
/// Represents the application configuration stored in the config file.
/// </summary>
public class AppConfig
{
    /// <summary>
    /// Gets or sets the API key for LLM communication.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the default model to use.
    /// </summary>
    public string? Model { get; set; }
}
