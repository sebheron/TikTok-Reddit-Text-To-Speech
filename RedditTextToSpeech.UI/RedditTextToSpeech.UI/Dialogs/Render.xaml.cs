using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RedditTextToSpeech.Core.Containers;
using RedditTextToSpeech.Logic.Factories;
using RedditTextToSpeech.Logic.Services;
using RedditTextToSpeech.UI.Enums;
using RedditTextToSpeech.UI.Models;
using RedditTextToSpeech.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.Dialogs
{
    public sealed partial class Render : ContentDialog
    {
        private readonly IAudioClipFactory audioClipFactory;
        private readonly IImageFactory imageFactory;
        private readonly IVideoFactory videoFactory;
        private readonly IVideoDownloadFactory videoDownloadFactory;

        public Render(IVideoService videoService,
            IImageService imageService,
            ISpeechSynthesisService speechSynthesisService,
            IVideoDownloadService videoDownloadService)
        {
            this.InitializeComponent();
            this.videoFactory = new VideoFactory(videoService);
            this.imageFactory = new ImageFactory(imageService);
            this.audioClipFactory = new AudioClipFactory(speechSynthesisService);
            this.videoDownloadFactory = new VideoDownloadFactory(videoDownloadService);
            this.ProjectsToRender = new List<IProject>();
        }

        public IList<IProject> ProjectsToRender { get; }

        public void RenderVideo(object sender, RoutedEventArgs eventArgs)
        {
            if (this.ProjectsToRender.Count == 0
                || String.IsNullOrEmpty(this.OutputPath.Text)
                || String.IsNullOrEmpty(this.BackgroundURL.Text)) return;
            Task.Run(async () =>
            {
                foreach (var project in this.ProjectsToRender)
                {
                    var values = new List<AudioImagePair>();
                    try
                    {
                        foreach (var entry in project.Entries)
                        {
                            var image = entry.SelectedEntryType switch
                            {
                                EntryType.Title => await this.imageFactory.GetImage(new Post(entry.SubredditName, entry.CommenterName, entry.Text)),
                                EntryType.Comment => await this.imageFactory.GetImage(new Comment(entry.CommenterName, entry.Text)),
                                EntryType.Text => await this.imageFactory.GetImage(entry.Text),
                                _ => null
                            };
                            var audio = await this.audioClipFactory.GetAudioClip(entry.Text, entry.SelectedVoice.Name);
                            values.Add(new AudioImagePair(audio, image));
                        }

                        var backgroundVideo = await this.videoDownloadFactory.DownloadFromURL(this.BackgroundURL.Text);

                        var video = await this.videoFactory.GetVideo(values, TimeSpan.Zero, backgroundVideo, this.OutputPath.Text);

                        foreach (var value in values)
                        {
                            File.Delete(value.ImagePath);
                            File.Delete(value.AudioPath);
                        }

                        if (video != null)
                        {
                            new Process() { StartInfo = new ProcessStartInfo(video) { UseShellExecute = true } }.Start();
                        }
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
                    finally
                    {
                        this.Hide();
                    }
                }
            }).Wait();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
