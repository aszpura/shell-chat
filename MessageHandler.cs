namespace shell_chat;

public class MessageHandler
{
    public void ProcessMessage(string? message)
    {
        if (string.IsNullOrEmpty(message))
        {
            Console.WriteLine("No message provided. Use --help for more information.");
            return;
        }

        Console.WriteLine($"Processing message: {message}");
    }
}
