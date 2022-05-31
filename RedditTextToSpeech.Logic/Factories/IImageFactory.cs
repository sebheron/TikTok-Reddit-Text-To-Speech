using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The image factory interface.
    /// </summary>
    public interface IImageFactory
    {
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="text">The display text.</param>
        /// <param name="username">Username to display.</param>
        /// <param name="subreddit">Subreddit to display.</param>
        /// <param name="avatar">The link to the saved avatar.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string text, string username, string subreddit, string avatar);

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="title">The display title.</param>
        /// <param name="username">Username to display.</param>
        /// <param name="subreddit">Subreddit to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string title, string username, string subreddit);

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="text">The display text.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string text);
    }
}