using RedditTextToSpeech.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RedditTextToSpeech.Core.Content
{
    /// <summary>
    /// Class which contains specific post information retrieved from reddit.
    /// If information is not available it will be blank.
    /// </summary>
    public class Post : IPost
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
        public Post(string subreddit, string username, string title, IList<string> content, string icon, string flair, string url)
        {
            Subreddit = subreddit;
            Username = username;
            Title = title;
            Content = content;
            Image = icon;
            Comments = new List<Comment>();
            Flair = flair;
            Url = url;
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

        /// <summary>
        /// The URL of the post.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// First content entry.
        /// </summary>
        public string First => this.Content?.FirstOrDefault() ?? string.Empty;
    }
}