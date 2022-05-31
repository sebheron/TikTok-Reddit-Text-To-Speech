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
    public class RedditService : IRedditService
    {
        public Post GetPostInformation(string url, int? commentCount = 0)
        {
            //Get JSON from URL and parse.
            var postJson = this.GetJsonStringFromURL($"{url}.json");
            var array = JArray.Parse(postJson);
            if (!array.HasValues) throw new ArgumentException();

            //Parse post details.
            var postData = array[0]?.SelectTokens("$..children").First().SelectToken("$..data");
            if (postData == null) throw new NullReferenceException("Post is null");
            var post = this.ParsePost(postData);

            if (array.Count <= 1) return post;

            //Parse comments details.
            var comments = array[1]?.SelectTokens("$..children[?(@.kind == 't1')]").ToArray();
            if (comments == null) throw new NullReferenceException("Comments are null");
            for (int i = 0; i < commentCount && i < comments.Length; i++)
            {
                var commentData = comments[i].SelectToken("$.data");
                if (commentData == null) throw new NullReferenceException("Comment is null");
                post.Comments.Add(this.ParseComment(commentData));
            }

            return post;
        }

        private Post ParsePost(JToken postData)
        {
            var subreddit = postData.SelectToken("$.subreddit")?.Value<string>();
            var username = postData.SelectToken("$.author")?.Value<string>();
            var postTitle = postData.SelectToken("$.title")?.Value<string>();
            var content = postData.SelectToken("$.selftext")?.Value<string>()?.Trim()
                .Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));
            var flair = postData.SelectToken("$.link_flair_text")?.Value<string>();

            if (content == null) throw new Exception();
            if (subreddit == null) throw new Exception();

            return new Post(subreddit,
                username,
                postTitle,
                this.GetParagraphs(content),
                this.GetIconUrl(subreddit),
                flair);
        }

        private Comment ParseComment(JToken commentData)
        {
            var username = commentData.SelectToken("$.author")?.Value<string>();
            var content = commentData.SelectToken("$.body")?.Value<string>()?.Trim()
                .Split('\n').Where(x => !string.IsNullOrWhiteSpace(x));
            var flair = commentData.SelectToken("$.author_flair_text")?.Value<string>();

            if (username == null) throw new Exception();
            if (content == null) throw new Exception();

            return new Comment(this.GetAvatarUrl(username),
                username,
                this.GetParagraphs(content),
                flair);
        }

        private string? GetAvatarUrl(string username)
        {
            if (username != "[deleted]")
            {
                var json = this.GetJsonStringFromURL($"https://www.reddit.com/user/{username}/about.json");
                var about = JObject.Parse(json);
                return this.DownloadImage(about.SelectTokens("$..icon_img")?.First()?.Value<string>());
            }
            return null;
        }

        private string? GetIconUrl(string subreddit)
        {
            var json = this.GetJsonStringFromURL($"https://www.reddit.com/r/{subreddit}/about.json");
            var about = JObject.Parse(json);
            return this.DownloadImage(about.SelectToken("$..icon_img")?.Value<string>());
        }

        private string? DownloadImage(string url)
        {
            var path = $"{Guid.NewGuid()}.png";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url.Split("?")[0]), path);
            }
            return path;
        }

        private IList<string> GetParagraphs(IEnumerable<string> content)
        {
            var displayParagraphs = new List<string>();
            foreach (var paragraph in content)
            {
                displayParagraphs.AddRange(this.SubsectionParagraphs(paragraph.Trim()));
            }
            return displayParagraphs;
        }

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


        private string GetJsonStringFromURL(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
