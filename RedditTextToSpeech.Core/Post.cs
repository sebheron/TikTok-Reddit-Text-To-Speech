using System.Collections.Generic;

namespace RedditTextToSpeech.Core
{
    public class Post
    {
        public Post(string? subreddit, string? username, string? title, IList<string>? content, string? icon, string? flair)
        {
            this.Subreddit = subreddit;
            this.Username = username;
            this.Title = title;
            this.Content = content;
            this.Image = icon;
            this.Comments = new List<Comment>();
            this.Flair = flair;
        }

        public IList<Comment> Comments { get; }
        public IList<string>? Content { get; }
        public string? Flair { get; }
        public string? Image { get; }
        public string? Subreddit { get; }
        public string? Title { get; }
        public string? Username { get; }
    }
}