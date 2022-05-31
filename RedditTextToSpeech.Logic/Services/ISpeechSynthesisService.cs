using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public interface ISpeechSynthesisService
    {
        string Extension { get; }

        string[] FemaleVoices { get; }

        string[] MaleVoices { get; }

        Task<string> GetSound(string path, string voice, string text);
    }
}