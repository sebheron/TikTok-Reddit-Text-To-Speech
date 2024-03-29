﻿using RedditTextToSpeech.Logic.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    /// <summary>
    /// The audio clip factory used for creating audio clips.
    /// </summary>
    public class AudioClipFactory : IAudioClipFactory
    {
        public ISpeechSynthesisService synthesisService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioClipFactory"/> class.
        /// </summary>
        /// <param name="synthesisService">The speech synthesis service.</param>
        public AudioClipFactory(ISpeechSynthesisService synthesisService)
        {
            this.synthesisService = synthesisService;
        }

        /// <summary>
        /// Gets the audio clip.
        /// </summary>
        /// <param name="text">The text to read.</param>
        /// <param name="voice">The voice.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetAudioClip(string text, string voice)
        {
            var path = $"{Guid.NewGuid()}{this.synthesisService.Extension}";
            return Path.GetFullPath(await this.synthesisService.GetSound(path, voice, text));
        }
    }
}