using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace RedditTextToSpeech.Logic.Services
{
    public class LibVideoDownloadService : IVideoDownloadService
    {
        public string Extension => ".mp4";

        public async Task<string> DownloadFromURL(string url, string output)
        {
            if (File.Exists(output)) File.Delete(output);
            var yt = YouTube.Default;
            var video = await yt.GetVideoAsync(url);
            File.WriteAllBytes(output, await video.GetBytesAsync());
            return output;
        }
    }
}
