using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The video service interface.
    /// </summary>
    public interface IVideoService
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the video.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="values">The audio image values.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="background">The background video.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetVideo(string path, IList<AudioImagePair> values, TimeSpan startTime, string background);
    }
}