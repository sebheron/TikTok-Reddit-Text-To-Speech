using Newtonsoft.Json.Linq;
using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The reddit service.
    /// Connects to reddit.com and retrieves basic information about posts and comments.
    /// </summary>
    public class RedditService : IRedditService
    {
        /// <summary>
        /// Gets the post information as a <see cref="Post"/> object.
        /// </summary>
        /// <param name="url">The url of the reddit post.</param>
        /// <param name="commentCount">The number of comments to collect.</param>
        /// <returns>Post object containing post information.</returns>
        public Post GetPostInformation(string url, int? commentCount, int? commentsToSkip)
        {
            //Get JSON from URL and parse.
            var postJson = this.GetJsonStringFromURL($"{url}.json");
            var array = JArray.Parse(postJson);
            if (!array.HasValues) throw new ArgumentException();

            //Parse post details.
            var postData = array[0]?.SelectTokens("$..children").First().SelectToken("$..data");
            if (postData == null) throw new NullReferenceException("Post is null");
            var post = this.ParsePost(postData, url);

            if (array.Count <= 1) return post;

            //Parse comments details.
            var comments = array[1]?.SelectTokens("$..children[?(@.kind == 't1')]").ToArray();
            if (comments == null) throw new NullReferenceException("Comments are null");
            for (int i = (commentCount ?? 0); i < (commentCount ?? 0) + (commentsToSkip ?? 0) && i < comments.Length; i++)
            {
                var commentData = comments[i].SelectToken("$.data");
                if (commentData == null) throw new NullReferenceException("Comment is null");
                post.Comments.Add(this.ParseComment(commentData, post));
            }

            return post;
        }

        /// <summary>
        /// Gets the avatar url.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Avatar url.</returns>
        private string GetAvatarUrl(string username)
        {
            if (username != "[deleted]")
            {
                var json = this.GetJsonStringFromURL($"https://www.reddit.com/user/{username}/about.json");
                var about = JObject.Parse(json);
                return about.SelectTokens("$..icon_img")?.First()?.Value<string>() ?? String.Empty;
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the icon url.
        /// </summary>
        /// <param name="subreddit">The subreddit.</param>
        /// <returns>Subreddit icon url.</returns>
        private string GetIconUrl(string subreddit)
        {
            var json = this.GetJsonStringFromURL($"https://www.reddit.com/r/{subreddit}/about.json");
            var about = JObject.Parse(json);
            return about.SelectToken("$..icon_img")?.Value<string>() ?? String.Empty;
        }

        /// <summary>
        /// Sends a HTTP request for JSON data for the reddit page.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>Unparsed JSON data.</returns>
        private string GetJsonStringFromURL(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Gets the display paragraphs. Display paragraphs are limited to a length to show less
        /// text on screen.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>Sensible partitions of paragraphs.</returns>
        private IList<string> GetParagraphs(IEnumerable<string> content)
        {
            var displayParagraphs = new List<string>();
            foreach (var paragraph in content)
            {
                displayParagraphs.AddRange(this.SubsectionParagraphs(paragraph.Trim()));
            }
            return displayParagraphs;
        }

        /// <summary>
        /// Parses the comment.
        /// </summary>
        /// <param name="commentData">The comment data JToken.</param>
        /// <exception cref="Exception">Throws an exception if the content can't be retrieved.</exception>
        /// <returns>A Comment instance.</returns>
        private Comment ParseComment(JToken commentData, Post post)
        {
            var username = commentData.SelectToken("$.author")?.Value<string>();
            var content = commentData.SelectToken("$.body")?.Value<string>()?.Trim()
                .Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));
            var flair = commentData.SelectToken("$.author_flair_text")?.Value<string>() ?? String.Empty;

            if (username == null || content == null) throw new NullReferenceException();

            return new Comment(post,
                this.GetAvatarUrl(username),
                username,
                this.GetParagraphs(content),
                flair);
        }

        /// <summary>
        /// Parses the post.
        /// </summary>
        /// <param name="postData">The post data JToken.</param>
        /// <exception cref="Exception">Throws an exception if the content can't be retrieved.</exception>
        /// <returns>A Post instance.</returns>
        private Post ParsePost(JToken postData, string url)
        {
            var subreddit = postData.SelectToken("$.subreddit")?.Value<string>();
            var username = postData.SelectToken("$.author")?.Value<string>();
            var postTitle = postData.SelectToken("$.title")?.Value<string>();
            var content = postData.SelectToken("$.selftext")?.Value<string>()?.Trim()
                .Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));
            var flair = postData.SelectToken("$.link_flair_text")?.Value<string>() ?? "";

            if (content == null || subreddit == null || username == null || postTitle == null || flair == null) throw new NullReferenceException();

            return new Post(subreddit,
                username,
                postTitle,
                this.GetParagraphs(content),
                this.GetIconUrl(subreddit),
                flair,
                url);
        }

        /// <summary>
        /// Subsections the paragraphs up into reasonble chunks.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        /// <returns>List of subsections.</returns>
        private IList<string> SubsectionParagraphs(string paragraph)
        {
            var paragraphs = new List<string>();
            if (paragraph.Length > 350)
            {
                var sentences = paragraph.Split('.', ';');
                if (sentences[0].Length > 350)
                {
                    sentences = paragraph.Split(',');
                }
                var sb = new StringBuilder();
                foreach (var sentence in sentences)
                {
                    if (sb.Length + sentence.Length + 1 > 350) break;
                    sb.Append(sentence + ".");
                }

                if (!Regex.IsMatch(paragraph, "[Tt][Ll]([;:]?)[Dd][Rr]"))
                {
                    paragraphs.Add(sb.ToString().Trim());
                }

                paragraphs.AddRange(SubsectionParagraphs(paragraph[sb.Length..]));
            }
            else if (!Regex.IsMatch(paragraph, "[Tt][Ll]([;:]?)[Dd][Rr]"))
            {
                paragraphs.Add(paragraph.Trim());
            }
            return paragraphs;
        }
    }
}