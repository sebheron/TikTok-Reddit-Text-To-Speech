using System.Collections.Generic;

namespace RedditTextToSpeech.Core
{
    /// <summary>
    /// The comment.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Instance of a comment
        /// </summary>
        /// <param name="image"></param>
        /// <param name="username"></param>
        /// <param name="content"></param>
        /// <param name="flair"></param>
        public Comment(string image, string username, IList<string> content, string flair)
        {
            this.Image = image;
            this.Username = username;
            this.Content = content;
            this.Flair = flair;
        }

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
        /// Gets the username.
        /// </summary>
        public string Username { get; }
    }
}