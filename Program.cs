using System.CommandLine;
using shell_chat;

var rootCommand = new RootCommand("Shell Chat App - A CLI tool for communicating with LLM models");
rootCommand.Options.Add(CommandConfiguration.MessageOption);
rootCommand.Options.Add(CommandConfiguration.ApiKeyOption);
rootCommand.Subcommands.Add(ApiConfigCommand.Configure());
rootCommand.SetAction(CommandConfiguration.HandleMessageCommand);

return rootCommand.Parse(args).Invoke();

