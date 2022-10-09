using System;
using System.Collections.Generic;
using System.Text;

namespace RedditTextToSpeech.Core.Interfaces
{
    public interface IPost : IComment
    {
        public string Title { get; }
        public string Subreddit { get; }
    }
}
