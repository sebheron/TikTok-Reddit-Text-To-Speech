using System.Collections.Generic;

namespace RedditTextToSpeech.Core
{
    /// <summary>
    /// The post class.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        /// <param name="subreddit">The subreddit.</param>
        /// <param name="username">The username.</param>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="flair">The flair.</param>
        public Post(string subreddit, string username, string title, IList<string> content, string icon, string flair)
        {
            this.Subreddit = subreddit;
            this.Username = username;
            this.Title = title;
            this.Content = content;
            this.Image = icon;
            this.Comments = new List<Comment>();
            this.Flair = flair;
        }

        /// <summary>
        /// Gets the comments for the post.
        /// </summary>
        public IList<Comment> Comments { get; }
        /// <summary>
        /// Gets the content.
        /// </summary>
        public IList<string> Content { get; }
        /// <summary>
        /// Gets the flair.
        /// </summary>
        public string Flair { get; }
        /// <summary>
        /// Gets the image.
        /// </summary>
        public string Image { get; }
        /// <summary>
        /// Gets the subreddit.
        /// </summary>
        public string Subreddit { get; }
        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; }
    }
}