using RedditTextToSpeech.Core;
using System;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public interface ISpeechSynthesisService
    {
        Task<string> GetSound(string path, string voice, string text);

        string[] MaleVoices { get; }

        string[] FemaleVoices { get; }

        string Extension { get; }
    }
}
