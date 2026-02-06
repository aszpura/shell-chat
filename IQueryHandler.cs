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
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ProcessQueryAsync(string? query, string? apiKey = null, CancellationToken cancellationToken = default);
}
