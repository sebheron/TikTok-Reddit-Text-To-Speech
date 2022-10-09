using System;
using System.Collections.Generic;
using System.Text;

namespace RedditTextToSpeech.Core.Interfaces
{
    public interface IComment
    {
        public string Username { get; }

        public string First { get; }
    }
}
