using RedditTextToSpeech.Core;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The reddit service interface.
    /// </summary>
    public interface IRedditService
    {
        /// <summary>
        /// Gets the post information as a <see cref="Post"/> object.
        /// </summary>
        /// <param name="url">The url of the reddit post.</param>
        /// <param name="commentCount">The number of comments to collect.</param>
        /// <returns>Post object containing post information.</returns>
        public Post GetPostInformation(string url, int? commentCount = 0, int? commentsToSkip = 0);
    }
}