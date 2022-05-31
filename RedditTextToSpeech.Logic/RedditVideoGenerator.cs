using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic.Factories;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic
{
    public class RedditVideoGenerator
    {
        private IVideoFactory videoFactory;
        private IImageFactory imageFactory;
        private IAudioClipFactory audioClipFactory;
        private ISpeechSynthesisService speechSynthesisService;
        private IRedditService redditService;

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
        /// <returns>The path to the video produced.</returns>
        public async Task<string> GenerateVideo(string url, string backgroundVideo, Gender gender, TimeSpan startTime)
        {
            try
            {
                var post = this.redditService.GetPostInformation(url);
                var voices = gender == Gender.Male ? this.speechSynthesisService.MaleVoices : this.speechSynthesisService.FemaleVoices;
                var voice = voices[new Random().Next(voices.Length)];

                var values = new List<AudioImagePair>();
                var image = await this.imageFactory.GetImage(post.Title, post.Username, post.Subreddit);
                var audio = await this.audioClipFactory.GetAudioClip(post.Title, voice);
                values.Add(new AudioImagePair(audio, image));

                foreach (var content in post.Content)
                {
                    var contentImage = await this.imageFactory.GetImage(content);
                    var contentAudio = await this.audioClipFactory.GetAudioClip(content, voice);
                    values.Add(new AudioImagePair(contentAudio, contentImage));
                }

                var video = await this.videoFactory.GetVideo(values, startTime, backgroundVideo);

                File.Delete("output.mp4");
                foreach (var value in values)
                {
                    File.Delete(value.ImagePath);
                    File.Delete(value.AudioPath);
                }

                return video;
            }
            catch (Exception e)
            {
                throw new Exception("Reddit Video Generator error. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Generates a comment video from a url.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GenerateVideo(string url, string backgroundVideo, Gender gender, TimeSpan startTime, int commentsToHarvest)
        {
            try
            {
                var post = this.redditService.GetPostInformation(url, commentsToHarvest);
                var voices = gender == Gender.Male ? this.speechSynthesisService.MaleVoices : this.speechSynthesisService.FemaleVoices;
                var voice = voices[new Random().Next(voices.Length)];

                var values = new List<AudioImagePair>();
                var image = await this.imageFactory.GetImage(post.Title, post.Username, post.Subreddit);
                var audio = await this.audioClipFactory.GetAudioClip(post.Title, voice);
                values.Add(new AudioImagePair(audio, image));

                foreach (var comment in post.Comments)
                {
                    var contentImage = await this.imageFactory.GetImage(comment.Content[0], comment.Username, post.Subreddit, comment.Image);
                    var contentAudio = await this.audioClipFactory.GetAudioClip(comment.Content[0], voice);
                    values.Add(new AudioImagePair(contentAudio, contentImage));
                    for (int i = 1; i < comment.Content.Count; i++)
                    {
                        var commentImage = await this.imageFactory.GetImage(comment.Content[i]);
                        var commentAudio = await this.audioClipFactory.GetAudioClip(comment.Content[i], voice);
                        values.Add(new AudioImagePair(commentAudio, commentImage));
                    }
                }

                var video = await this.videoFactory.GetVideo(values, startTime, backgroundVideo);

                File.Delete("output.mp4");
                foreach (var value in values)
                {
                    File.Delete(value.ImagePath);
                    File.Delete(value.AudioPath);
                }

                return video;
            }
            catch (Exception e)
            {
                throw new Exception("Reddit Video Generator error. See inner exception for details.", e);
            }
        }
    }
}
