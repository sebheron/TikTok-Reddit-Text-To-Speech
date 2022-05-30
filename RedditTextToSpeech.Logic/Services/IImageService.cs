using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Gets a image representing a reddit title.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        Task<string> GetImage(string path, string title, string username, string subreddit);
        
        Task<string> GetImage(string path, string text);

        string Extension { get; }
    }
}
