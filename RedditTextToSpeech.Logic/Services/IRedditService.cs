using Newtonsoft.Json.Linq;
using RedditTextToSpeech.Core;

namespace RedditTextToSpeech.Logic.Services
{
    internal interface IRedditService
    {
        public Post GetPostInformation(string url);
    }
}
