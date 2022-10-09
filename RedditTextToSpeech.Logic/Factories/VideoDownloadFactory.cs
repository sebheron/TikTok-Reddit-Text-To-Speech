using Newtonsoft.Json.Linq;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public class VideoDownloadFactory : IVideoDownloadFactory
    {
        private IVideoDownloadService videoDownloadService;

        public VideoDownloadFactory(IVideoDownloadService videoDownloadService)
        {
            this.videoDownloadService = videoDownloadService;
        }

        public async Task<string> DownloadFromURL(string url)
        {
            var path = $"{Guid.NewGuid()}{this.videoDownloadService.Extension}";
            return Path.GetFullPath(await this.videoDownloadService.DownloadFromURL(url, path));
        }
    }
}
