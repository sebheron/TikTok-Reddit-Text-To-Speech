using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public interface IAudioClipFactory
    {
        Task<string> GetAudioClip(string text, string voice);
    }
}