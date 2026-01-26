using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using shell_chat;
using shell_chat.Configuration;

// Build DI container
var services = new ServiceCollection();
services.AddSingleton<IConfigurationManager, ConfigurationManager>();
services.AddSingleton<IMessageHandler, MessageHandler>();
services.AddSingleton<IQueryHandler, QueryHandler>();
services.AddSingleton<ApiConfigCommand>();
services.AddSingleton<CommandConfiguration>();

var serviceProvider = services.BuildServiceProvider();

// Resolve and configure root command
var commandConfig = serviceProvider.GetRequiredService<CommandConfiguration>();
var rootCommand = commandConfig.CreateRootCommand();

return rootCommand.Parse(args).Invoke();

