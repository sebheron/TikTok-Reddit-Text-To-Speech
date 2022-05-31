using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The video factory interface.
    /// </summary>
    public interface IVideoFactory
    {
        /// <summary>
        /// Gets the video.
        /// </summary>
        /// <param name="values">The audio image values.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="background">The background.</param>
        /// <returns>Awaitable task returning string.</returns>
        Task<string> GetVideo(IList<AudioImagePair> values, TimeSpan startTime, string background);
    }
}