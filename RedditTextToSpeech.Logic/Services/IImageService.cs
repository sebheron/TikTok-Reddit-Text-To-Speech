using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public interface IImageService
    {
        Task<string> GetImage(string path, string title, string username, string subreddit);
        Task<string> GetImage(string path, string text);
    }
}
