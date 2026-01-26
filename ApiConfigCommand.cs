using System.CommandLine;
using shell_chat.Configuration;

namespace shell_chat;

/// <summary>
/// Handles the 'config' subcommand for managing application configuration.
/// </summary>
public static class ApiConfigCommand
{
    /// <summary>
    /// Creates the config command with its subcommands.
    /// </summary>
    public static Command Configure()
    {
        var configCommand = new Command("config", "Manage shell-chat configuration");

        configCommand.Subcommands.Add(CreateSetCommand());
        configCommand.Subcommands.Add(CreateShowCommand());
        configCommand.Subcommands.Add(CreateClearCommand());

        return configCommand;
    }

    private static Command CreateSetCommand()
    {
        var apiKeyArgument = new Argument<string>("api-key")
        {
            Description = "The API key to store"
        };
        
        var setCommand = new Command("set-key", "Set the API key in the config file");
        setCommand.Arguments.Add(apiKeyArgument);

        setCommand.SetAction(parseResult =>
        {
            var apiKey = parseResult.GetValue(apiKeyArgument);
            
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("Error: API key cannot be empty.");
                return 1;
            }

            var configManager = new ConfigurationManager();
            configManager.SetApiKey(apiKey);

            // Show masked key for confirmation
            var maskedKey = MaskApiKey(apiKey);
            Console.WriteLine($"API key saved to: {configManager.ConfigFilePath}");
            Console.WriteLine($"Key: {maskedKey}");
            Console.WriteLine();
            Console.WriteLine("Warning: The API key is stored in plain text. Ensure the config file has appropriate permissions.");

            return 0;
        });

        return setCommand;
    }

    private static Command CreateShowCommand()
    {
        var showCommand = new Command("show", "Show current configuration and API key source");

        showCommand.SetAction(parseResult =>
        {
            var configManager = new ConfigurationManager();
            var source = configManager.GetApiKeySource();
            var hasKey = configManager.HasApiKey();

            Console.WriteLine("Shell-Chat Configuration");
            Console.WriteLine("========================");
            Console.WriteLine();
            Console.WriteLine($"Config file location: {configManager.ConfigFilePath}");
            Console.WriteLine($"Config file exists:   {File.Exists(configManager.ConfigFilePath)}");
            Console.WriteLine();
            Console.WriteLine($"API Key configured:   {(hasKey ? "Yes" : "No")}");
            Console.WriteLine($"API Key source:       {source}");

            if (hasKey)
            {
                var apiKey = configManager.ResolveApiKey();
                Console.WriteLine($"API Key (masked):     {MaskApiKey(apiKey!)}");
            }

            Console.WriteLine();
            Console.WriteLine("Priority order: --api-key argument > SHELLCHAT_API_KEY env var > config file");

            return 0;
        });

        return showCommand;
    }

    private static Command CreateClearCommand()
    {
        var clearCommand = new Command("clear-key", "Clear the API key from the config file");

        clearCommand.SetAction(parseResult =>
        {
            var configManager = new ConfigurationManager();
            configManager.ClearApiKey();

            Console.WriteLine("API key cleared from config file.");
            Console.WriteLine();
            Console.WriteLine("Note: This does not affect the SHELLCHAT_API_KEY environment variable.");

            return 0;
        });

        return clearCommand;
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
