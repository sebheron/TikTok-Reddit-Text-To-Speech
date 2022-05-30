using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public class AudioClipFactory : IAudioClipFactory
    {
        public ISpeechSynthesisService synthesisService;

        public AudioClipFactory(ISpeechSynthesisService synthesisService)
        {
            this.synthesisService = synthesisService;
        }

        public async Task<string> GetAudioClip(string text, string voice)
        {
            var path = $"{Guid.NewGuid().ToString()}";
            var file = await this.synthesisService.GetSound(path, voice, text);
            return path;
        }
    }
}
