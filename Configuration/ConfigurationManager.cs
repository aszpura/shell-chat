using System.Text.Json;

namespace shell_chat.Configuration;

/// <summary>
/// Manages application configuration with a layered approach:
/// 1. Command-line arguments (highest priority)
/// 2. Environment variables
/// 3. User config file (lowest priority)
/// </summary>
public class ConfigurationManager : IConfigurationManager
{
    private const string EnvironmentVariableName = "SHELLCHAT_API_KEY";
    private const string ConfigFileName = "config.json";
    private const string AppFolderName = "shell-chat";

    private readonly string _configFilePath;
    private AppConfig? _cachedConfig;

    public ConfigurationManager()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var configDirectory = Path.Combine(appDataPath, AppFolderName);
        _configFilePath = Path.Combine(configDirectory, ConfigFileName);
    }

    /// <summary>
    /// Gets the configuration directory path.
    /// </summary>
    public string ConfigDirectory => Path.GetDirectoryName(_configFilePath)!;

    /// <summary>
    /// Gets the configuration file path.
    /// </summary>
    public string ConfigFilePath => _configFilePath;

    /// <summary>
    /// Resolves the API key using the layered approach.
    /// Priority: 1. Command-line argument, 2. Environment variable, 3. Config file
    /// </summary>
    /// <param name="commandLineApiKey">API key provided via command-line argument.</param>
    /// <returns>The resolved API key, or null if not found.</returns>
    public string? ResolveApiKey(string? commandLineApiKey = null)
    {
        // Priority 1: Command-line argument
        if (!string.IsNullOrWhiteSpace(commandLineApiKey))
        {
            return commandLineApiKey;
        }

        // Priority 2: Environment variable
        var envApiKey = Environment.GetEnvironmentVariable(EnvironmentVariableName);
        if (!string.IsNullOrWhiteSpace(envApiKey))
        {
            return envApiKey;
        }

        // Priority 3: Config file
        var config = LoadConfig();
        return config?.ApiKey;
    }

    /// <summary>
    /// Loads the configuration from the config file.
    /// </summary>
    /// <returns>The loaded configuration, or null if the file doesn't exist.</returns>
    public AppConfig? LoadConfig()
    {
        if (_cachedConfig != null)
        {
            return _cachedConfig;
        }

        if (!File.Exists(_configFilePath))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(_configFilePath);
            _cachedConfig = JsonSerializer.Deserialize<AppConfig>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return _cachedConfig;
        }
        catch (JsonException)
        {
            Console.Error.WriteLine($"Warning: Could not parse config file at {_configFilePath}");
            return null;
        }
    }

    /// <summary>
    /// Saves the configuration to the config file.
    /// </summary>
    /// <param name="config">The configuration to save.</param>
    public void SaveConfig(AppConfig config)
    {
        var directory = Path.GetDirectoryName(_configFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_configFilePath, json);
        _cachedConfig = config;
    }

    /// <summary>
    /// Sets the API key in the config file.
    /// </summary>
    /// <param name="apiKey">The API key to store.</param>
    public void SetApiKey(string apiKey)
    {
        var config = LoadConfig() ?? new AppConfig();
        config.ApiKey = apiKey;
        SaveConfig(config);
    }

    /// <summary>
    /// Clears the API key from the config file.
    /// </summary>
    public void ClearApiKey()
    {
        var config = LoadConfig();
        if (config != null)
        {
            config.ApiKey = null;
            SaveConfig(config);
        }
    }

    /// <summary>
    /// Gets information about where the API key is being resolved from.
    /// </summary>
    /// <param name="commandLineApiKey">API key provided via command-line argument.</param>
    /// <returns>A string describing the source of the API key.</returns>
    public string GetApiKeySource(string? commandLineApiKey = null)
    {
        if (!string.IsNullOrWhiteSpace(commandLineApiKey))
        {
            return "command-line argument";
        }

        var envApiKey = Environment.GetEnvironmentVariable(EnvironmentVariableName);
        if (!string.IsNullOrWhiteSpace(envApiKey))
        {
            return $"environment variable ({EnvironmentVariableName})";
        }

        var config = LoadConfig();
        if (!string.IsNullOrWhiteSpace(config?.ApiKey))
        {
            return $"config file ({_configFilePath})";
        }

        return "not configured";
    }

    /// <summary>
    /// Checks if an API key is configured from any source.
    /// </summary>
    /// <param name="commandLineApiKey">API key provided via command-line argument.</param>
    /// <returns>True if an API key is available.</returns>
    public bool HasApiKey(string? commandLineApiKey = null)
    {
        return !string.IsNullOrWhiteSpace(ResolveApiKey(commandLineApiKey));
    }
}
