using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The video download service interface.
    /// </summary>
    public interface IVideoDownloadService
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Downloads a video from a url.
        /// </summary>
        /// <param name="url">The url to download from.</param>
        /// <param name="output">The output path.</param>
        /// <returns>File path to downloaded video.</returns>
        Task<string> DownloadFromURL(string url, string output);
    }
}
