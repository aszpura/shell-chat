using System.CommandLine;
using shell_chat.Configuration;

namespace shell_chat;

/// <summary>
/// Provides command-line configuration including options and their associated actions.
/// </summary>
public class CommandConfiguration
{
    private readonly IConfigurationManager _configurationManager;
    private readonly IMessageHandler _messageHandler;
    private readonly IQueryHandler _queryHandler;
    private readonly ApiConfigCommand _apiConfigCommand;

    public CommandConfiguration(
        IConfigurationManager configurationManager,
        IMessageHandler messageHandler,
        IQueryHandler queryHandler,
        ApiConfigCommand apiConfigCommand)
    {
        _configurationManager = configurationManager;
        _messageHandler = messageHandler;
        _queryHandler = queryHandler;
        _apiConfigCommand = apiConfigCommand;
    }

    /// <summary>
    /// Creates and returns the configured root command.
    /// </summary>
    public RootCommand CreateRootCommand()
    {
        var rootCommand = new RootCommand("Shell Chat App - A CLI tool for communicating with LLM models");
        rootCommand.Subcommands.Add(CreateQueryCommand());
        rootCommand.Subcommands.Add(CreateMessageCommand());
        rootCommand.Subcommands.Add(_apiConfigCommand.CreateApiConfigCommand());
        return rootCommand;
    }

    /// <summary>
    /// Creates the query subcommand.
    /// </summary>
    private Command CreateQueryCommand()
    {
        var queryArgument = new Argument<string>("text")
        {
            Description = "The query text to send to the LLM."
        };

        var apiKeyOption = CreateApiKeyOption();

        var queryCommand = new Command("query", "Send a query to the LLM and get a response.");
        queryCommand.Aliases.Add("q");
        queryCommand.Arguments.Add(queryArgument);
        queryCommand.Options.Add(apiKeyOption);

        queryCommand.SetAction(parseResult =>
        {
            var query = parseResult.GetValue(queryArgument);
            var commandLineApiKey = parseResult.GetValue(apiKeyOption);
            var apiKey = _configurationManager.ResolveApiKey(commandLineApiKey);

            _queryHandler.ProcessQuery(query, apiKey);
            return 0;
        });

        return queryCommand;
    }

    /// <summary>
    /// Creates the message subcommand.
    /// </summary>
    private Command CreateMessageCommand()
    {
        var messageArgument = new Argument<string>("text")
        {
            Description = "The message text to process."
        };

        var apiKeyOption = CreateApiKeyOption();

        var messageCommand = new Command("message", "Process a message.");
        messageCommand.Aliases.Add("m");
        messageCommand.Arguments.Add(messageArgument);
        messageCommand.Options.Add(apiKeyOption);

        messageCommand.SetAction(parseResult =>
        {
            var message = parseResult.GetValue(messageArgument);
            var commandLineApiKey = parseResult.GetValue(apiKeyOption);
            var apiKey = _configurationManager.ResolveApiKey(commandLineApiKey);

            _messageHandler.ProcessMessage(message, apiKey);
            return 0;
        });

        return messageCommand;
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
}
