using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic.Factories;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic
{
    /// <summary>
    /// The reddit video generator.
    /// Requires services to be specified.
    /// Some built-in ones exist, however custom implementations can be provided by implementing the interfaces.
    /// </summary>
    public class RedditVideoGenerator
    {
        private readonly IAudioClipFactory audioClipFactory;
        private readonly IImageFactory imageFactory;
        private readonly IRedditService redditService;
        private readonly ISpeechSynthesisService speechSynthesisService;
        private readonly IVideoFactory videoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedditVideoGenerator"/> class.
        /// </summary>
        /// <param name="videoService">The video service.</param>
        /// <param name="imageService">The image service.</param>
        /// <param name="speechSynthesisService">The speech synthesis service.</param>
        /// <param name="redditService">The reddit service.</param>
        public RedditVideoGenerator(IVideoService videoService,
            IImageService imageService,
            ISpeechSynthesisService speechSynthesisService,
            IRedditService redditService)
        {
            this.videoFactory = new VideoFactory(videoService);
            this.imageFactory = new ImageFactory(imageService);
            this.audioClipFactory = new AudioClipFactory(speechSynthesisService);
            this.speechSynthesisService = speechSynthesisService;
            this.redditService = redditService;
        }

        /// <summary>
        /// Generates a post video from a url.
        /// </summary>
        /// <param name="url">The url of the reddit post.</param>
        /// <param name="backgroundVideo">The background video.</param>
        /// <param name="output">The output location.</param>
        /// <param name="gender">The gender of the TTS voice.</param>
        /// <param name="startTime">The starting time for the background video.</param>
        /// <returns>The path to the video produced.</returns>
        public async Task<string> GenerateVideo(string url, string backgroundVideo, string output, Gender gender, TimeSpan startTime)
        {
            var values = new List<AudioImagePair>();
            try
            {
                var post = this.redditService.GetPostInformation(url);
                var voices = gender == Gender.Male ? this.speechSynthesisService.MaleVoices : this.speechSynthesisService.FemaleVoices;
                var voice = voices[new Random().Next(voices.Length)];

                var image = await this.imageFactory.GetImage(post.Title, post.Username, post.Subreddit);
                var audio = await this.audioClipFactory.GetAudioClip(post.Title, voice);
                values.Add(new AudioImagePair(audio, image));

                foreach (var content in post.Content)
                {
                    var contentImage = await this.imageFactory.GetImage(content);
                    var contentAudio = await this.audioClipFactory.GetAudioClip(content, voice);
                    values.Add(new AudioImagePair(contentAudio, contentImage));
                }

                var video = await this.videoFactory.GetVideo(values, startTime, backgroundVideo, output);

                foreach (var value in values)
                {
                    File.Delete(value.ImagePath);
                    File.Delete(value.AudioPath);
                }

                return video;
            }
            catch (Exception e)
            {
                foreach (var value in values)
                {
                    File.Delete(value.ImagePath);
                    File.Delete(value.AudioPath);
                }
                throw new Exception("Reddit Video Generator error. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Generates a comment video from a url.
        /// </summary>
        /// <param name="url">The url of the reddit post.</param>
        /// <param name="backgroundVideo">The background video.</param>
        /// <param name="output">The output location.</param>
        /// <param name="gender">The gender of the TTS voice.</param>
        /// <param name="startTime">The starting time for the background video.</param>
        /// <param name="commentsToHarvest">The number of comments to add to the video.</param>
        /// <returns>The path to the video produced.</returns>
        public async Task<string> GenerateVideo(string url, string backgroundVideo, string output, Gender gender, TimeSpan startTime, int commentsToHarvest, bool alternateVoice)
        {
            var values = new List<AudioImagePair>();
            try
            {
                var post = this.redditService.GetPostInformation(url, commentsToHarvest);
                var voice = this.GetVoice(gender);

                var image = await this.imageFactory.GetImage(post.Title, post.Username, post.Subreddit);
                var audio = await this.audioClipFactory.GetAudioClip(post.Title, voice);
                values.Add(new AudioImagePair(audio, image));

                foreach (var comment in post.Comments)
                {
                    var contentImage = await this.imageFactory.GetImage(comment.Content[0], comment.Username);
                    var contentAudio = await this.audioClipFactory.GetAudioClip(comment.Content[0], voice);
                    values.Add(new AudioImagePair(contentAudio, contentImage));
                    for (int i = 1; i < comment.Content.Count; i++)
                    {
                        var commentImage = await this.imageFactory.GetImage(comment.Content[i]);
                        var commentAudio = await this.audioClipFactory.GetAudioClip(comment.Content[i], voice);
                        values.Add(new AudioImagePair(commentAudio, commentImage));
                    }
                    if (alternateVoice)
                    {
                        voice = this.GetVoice(gender);
                    }
                }

                var video = await this.videoFactory.GetVideo(values, startTime, backgroundVideo, output);

                foreach (var value in values)
                {
                    File.Delete(value.ImagePath);
                    File.Delete(value.AudioPath);
                }

                return video;
            }
            catch (Exception e)
            {
                foreach (var value in values)
                {
                    File.Delete(value.ImagePath);
                    File.Delete(value.AudioPath);
                }
                throw new Exception("Reddit Video Generator error. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Gets a random voice.
        /// </summary>
        /// <param name="gender">The gender of the speaker.</param>
        /// <returns>Voice name.</returns>
        private string GetVoice(Gender gender)
        {
            var voices = gender == Gender.Male ? this.speechSynthesisService.MaleVoices : this.speechSynthesisService.FemaleVoices;
            return voices[new Random().Next(voices.Length)]; ;
        }
    }
}