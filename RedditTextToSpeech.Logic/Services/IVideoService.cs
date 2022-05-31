using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public interface IVideoService
    {
        string Extension { get; }

        Task<string> GetVideo(string path, IList<AudioImagePair> values, TimeSpan startTime, string background);
    }
}