using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The image service interface.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets a image representing a reddit comment start.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="subreddit">The subreddit.</param>
        /// <param name="avatar">Link to users avatar.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string path, string text, string username);

        /// <summary>
        /// Gets a image representing a reddit title.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="subreddit">The subreddit.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string path, string title, string username, string subreddit);

        /// <summary>
        /// Gets a blank text image.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="text">The text to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string path, string text);
    }
}