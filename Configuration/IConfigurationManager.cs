namespace shell_chat.Configuration;

/// <summary>
/// Defines the contract for managing application configuration.
/// </summary>
public interface IConfigurationManager
{
    /// <summary>
    /// Gets the configuration directory path.
    /// </summary>
    string ConfigDirectory { get; }

    /// <summary>
    /// Gets the configuration file path.
    /// </summary>
    string ConfigFilePath { get; }

    /// <summary>
    /// Resolves the API key using the layered approach.
    /// Priority: 1. Command-line argument, 2. Environment variable, 3. Config file
    /// </summary>
    /// <param name="commandLineApiKey">API key provided via command-line argument.</param>
    /// <returns>The resolved API key, or null if not found.</returns>
    string? ResolveApiKey(string? commandLineApiKey = null);

    /// <summary>
    /// Loads the configuration from the config file.
    /// </summary>
    /// <returns>The loaded configuration, or null if the file doesn't exist.</returns>
    AppConfig? LoadConfig();

    /// <summary>
    /// Saves the configuration to the config file.
    /// </summary>
    /// <param name="config">The configuration to save.</param>
    void SaveConfig(AppConfig config);

    /// <summary>
    /// Sets the API key in the config file.
    /// </summary>
    /// <param name="apiKey">The API key to store.</param>
    void SetApiKey(string apiKey);

    /// <summary>
    /// Clears the API key from the config file.
    /// </summary>
    void ClearApiKey();

    /// <summary>
    /// Gets information about where the API key is being resolved from.
    /// </summary>
    /// <param name="commandLineApiKey">API key provided via command-line argument.</param>
    /// <returns>A string describing the source of the API key.</returns>
    string GetApiKeySource(string? commandLineApiKey = null);

    /// <summary>
    /// Checks if an API key is configured from any source.
    /// </summary>
    /// <param name="commandLineApiKey">API key provided via command-line argument.</param>
    /// <returns>True if an API key is available.</returns>
    bool HasApiKey(string? commandLineApiKey = null);
}
