using System.Collections.Generic;

namespace RedditTextToSpeech.Core
{
    public class Comment
    {
        public Comment(string? image, string? username, IList<string>? content, string? flair)
        {
            this.Image = image;
            this.Username = username;
            this.Content = content;
            this.Flair = flair;
        }

        public IList<string>? Content { get; }
        public string? Flair { get; }
        public string? Image { get; }
        public string? Username { get; }
    }
}