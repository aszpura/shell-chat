using System.CommandLine;
using shell_chat;

var rootCommand = new RootCommand("Shell Chat App");
rootCommand.Options.Add(CommandConfiguration.MessageOption);
rootCommand.SetAction(CommandConfiguration.HandleMessageCommand);

return rootCommand.Parse(args).Invoke();

