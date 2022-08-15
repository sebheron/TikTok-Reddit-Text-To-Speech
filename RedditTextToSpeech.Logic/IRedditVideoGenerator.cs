using RedditTextToSpeech.Core;
using System;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic
{
    /// <summary>
    /// The reddit video generator interface.
    /// </summary>
    internal interface IRedditVideoGenerator
    {
        /// <summary>
        /// Generates a post video from a url.
        /// </summary>
        /// <param name="url">The url of the reddit post.</param>
        /// <param name="backgroundVideo">The background video.</param>
        /// <param name="output">The output location.</param>
        /// <param name="gender">The gender of the TTS voice.</param>
        /// <param name="startTime">The starting time for the background video.</param>
        /// <returns>The path to the video produced.</returns>
        Task<string> GenerateVideo(string url, string backgroundVideo, string output, Gender gender, TimeSpan startTime);

        /// <summary>
        /// Generates a comment video from a url.
        /// </summary>
        /// <param name="url">The url of the reddit post.</param>
        /// <param name="backgroundVideo">The background video.</param>
        /// <param name="output">The output location.</param>
        /// <param name="gender">The gender of the TTS voice.</param>
        /// <param name="startTime">The starting time for the background video.</param>
        /// <param name="commentsToHarvest">The number of comments to add to the video.</param>
        /// <returns>The path to the video produced.</returns>
        Task<string> GenerateVideo(string url, string backgroundVideo, string output, Gender gender, TimeSpan startTime, int? commentsToHarvest, int? commentsToSkip, bool alternateVoice);
    }
}
