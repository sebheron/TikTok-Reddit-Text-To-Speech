using Newtonsoft.Json.Linq;
using RedditTextToSpeech.Core;

namespace RedditTextToSpeech.Logic.Services
{
    public interface IRedditService
    {
        public Post GetPostInformation(string url, int? commentCount = 0);
    }
}
