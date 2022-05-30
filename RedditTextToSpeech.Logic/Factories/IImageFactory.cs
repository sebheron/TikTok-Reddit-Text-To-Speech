using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public interface IImageFactory
    {
        Task<string> GetImage(string title, string username, string subreddit);
        Task<string> GetImage(string text);
    }
}
