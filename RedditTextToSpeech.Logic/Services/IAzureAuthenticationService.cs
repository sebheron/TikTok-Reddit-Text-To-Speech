using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The azure authentication service interface.
    /// </summary>
    internal interface IAzureAuthenticationService
    {
        /// <summary>
        /// Fetches the token asynchronously.
        /// </summary>
        /// <returns>Awaitable task returning token.</returns>
        Task<string> FetchTokenAsync();
    }
}