using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public interface IVideoService
    {
        Task<string> GetVideo(string path, IList<AudioImagePair> values, TimeSpan startTime, string background);

        string Extension { get; }
    }
}
