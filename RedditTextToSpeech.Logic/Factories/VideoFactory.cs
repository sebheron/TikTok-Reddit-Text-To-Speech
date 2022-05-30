using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    internal class VideoFactory : IVideoFactory
    {
        private IVideoService videoService;
        public VideoFactory(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        public async Task<string> GetVideo(IList<AudioImagePair> values, TimeSpan startTime, string background)
        {
            var path = Guid.NewGuid().ToString();
            var file = await this.videoService.GetVideo(path, values, startTime, background);
            return file;
        }
    }
}
