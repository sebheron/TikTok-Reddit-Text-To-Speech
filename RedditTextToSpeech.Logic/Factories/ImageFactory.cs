using RedditTextToSpeech.Logic.Services;
using System;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The image factory used for creating images.
    /// </summary>
    public class ImageFactory : IImageFactory
    {
        private IImageService imageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFactory"/> class.
        /// </summary>
        /// <param name="imageService">The image service.</param>
        public ImageFactory(IImageService imageService)
        {
            this.imageService = imageService;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="text">The display text.</param>
        /// <param name="username">Username to display.</param>
        /// <param name="subreddit">Subreddit to display.</param>
        /// <param name="avatar">The link to the saved avatar.</param>
        /// <returns>Awaitable task returning string.</returns>
        public async Task<string> GetImage(string text, string username, string subreddit, string avatar)
        {
            var path = Guid.NewGuid().ToString();
            var file = await this.imageService.GetImage(path, text, username, subreddit, avatar);
            return file;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="title">The display title.</param>
        /// <param name="username">Username to display.</param>
        /// <param name="subreddit">Subreddit to display.</param>
        /// <returns>Awaitable task returning string.</returns>
        public async Task<string> GetImage(string title, string username, string subreddit)
        {
            var path = Guid.NewGuid().ToString();
            var file = await this.imageService.GetImage(path, title, username, subreddit);
            return file;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="text">The display text.</param>
        /// <returns>Awaitable task returning string.</returns>
        public async Task<string> GetImage(string text)
        {
            var path = Guid.NewGuid().ToString();
            var file = await this.imageService.GetImage(path, text);
            return file;
        }
    }
}