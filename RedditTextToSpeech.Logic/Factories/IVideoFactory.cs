using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Factories
{
    public interface IVideoFactory
    {
        Task<string> GetVideo(IList<AudioImagePair> values, TimeSpan startTime, string background);
    }
}