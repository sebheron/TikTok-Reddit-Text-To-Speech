using RedditTextToSpeech.Logic.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using RedditTextToSpeech.Core.Interfaces;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The image factory used for creating images.
    /// </summary>
    public class ImageFactory : IImageFactory
    {
        private readonly IImageService imageService;

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
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetImage(IPost post)
        {
            var path = $"{Guid.NewGuid()}{this.imageService.Extension}";
            return Path.GetFullPath(await this.imageService.GetImage(path, post));
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="title">The display title.</param>
        /// <param name="username">Username to display.</param>
        /// <param name="subreddit">Subreddit to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetImage(IComment comment)
        {
            var path = $"{Guid.NewGuid()}{this.imageService.Extension}";
            return Path.GetFullPath(await this.imageService.GetImage(path, comment));
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="text">The display text.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetImage(string text)
        {
            var path = $"{Guid.NewGuid()}{this.imageService.Extension}";
            return Path.GetFullPath(await this.imageService.GetImage(path, text));
        }
    }
}