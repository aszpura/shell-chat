namespace shell_chat;

/// <summary>
/// Defines the contract for handling LLM queries.
/// </summary>
public interface IQueryHandler
{
    /// <summary>
    /// Processes a query using LLM.
    /// </summary>
    /// <param name="query">The query to send to the LLM.</param>
    /// <param name="apiKey">Optional API key for LLM communication.</param>
    void ProcessQuery(string? query, string? apiKey = null);
}
