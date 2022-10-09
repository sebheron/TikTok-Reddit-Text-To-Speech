using RedditTextToSpeech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.Models
{
    public class Post : IPost
    {
        public Post(string title, string subreddit, string username, string first = "")
        {
            this.Title = title;
            this.Subreddit = subreddit;
            this.Username = username;
            this.First = first;
        }

        public string Title { get; }

        public string Subreddit { get; }

        public string Username { get; }

        public string First { get; }
    }
}
