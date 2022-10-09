using RedditTextToSpeech.Core.Interfaces;
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
        /// Gets a image representing a reddit title.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="post">The post to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string path, IPost post);

        /// <summary>
        /// Gets a image representing a reddit comment start.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="comment">The comment to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string path, IComment comment);

        /// <summary>
        /// Gets a blank text image.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="text">The text to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetImage(string path, string text);
    }
}