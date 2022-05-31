using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The audio clip factory interface.
    /// </summary>
    public interface IAudioClipFactory
    {
        /// <summary>
        /// Gets the audio clip.
        /// </summary>
        /// <param name="text">The text to read.</param>
        /// <param name="voice">The voice.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetAudioClip(string text, string voice);
    }
}