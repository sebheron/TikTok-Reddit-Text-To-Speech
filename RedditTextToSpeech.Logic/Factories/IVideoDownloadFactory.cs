using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public interface IVideoDownloadFactory
    {
        /// <summary>
        /// Downloads a video from a url.
        /// </summary>
        /// <param name="url">The url to download from.</param>
        /// <returns>File path to downloaded video.</returns>
        Task<string> DownloadFromURL(string url);
    }
}
