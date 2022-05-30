using RedditTextToSpeech.Core;
using System;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public interface IAudioClipFactory
    {
        Task<FilePath> GetAudioClip(string text, string voice);
    }
}
