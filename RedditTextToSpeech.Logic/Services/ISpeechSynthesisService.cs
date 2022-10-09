﻿using RedditTextToSpeech.Logic.Containers;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The speech synthesis service interface.
    /// </summary>
    public interface ISpeechSynthesisService
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the voices.
        /// </summary>
        IVoice[] Voices { get; }

        /// <summary>
        /// Gets the sound.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="voice">The voice to use.</param>
        /// <param name="text">The text.</param>
        /// <returns>Awaitable task returning path.</returns>
        Task<string> GetSound(string path, string voice, string text);
    }
}