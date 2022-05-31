using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The video factory used for creating videos.
    /// </summary>
    internal class VideoFactory : IVideoFactory
    {
        private IVideoService videoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoFactory"/> class.
        /// </summary>
        /// <param name="videoService">The video service.</param>
        public VideoFactory(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        /// <summary>
        /// Gets the video.
        /// </summary>
        /// <param name="values">The audio image values.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="background">The background.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetVideo(IList<AudioImagePair> values, TimeSpan startTime, string background)
        {
            var path = Guid.NewGuid().ToString();
            var file = await this.videoService.GetVideo(path, values, startTime, background);
            return file;
        }
    }
}