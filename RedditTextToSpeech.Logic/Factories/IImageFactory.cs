using RedditTextToSpeech.Core;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public interface IImageFactory
    {
        Task<FilePath> GetImage(string title, string username, string subreddit);
        Task<FilePath> GetImage(string text);
    }
}
