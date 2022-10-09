using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using RedditTextToSpeech.Logic.Containers;
using RedditTextToSpeech.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedditTextToSpeech.UI.Dialogs
{
    public sealed partial class Settings : ContentDialog
    {
        public string URL { get; private set; }

        private ISpeechSynthesisService[] speechSynthesisServices;
        private string voiceService;
        public string VoiceService
        {
            get => this.voiceService;
            set
            {
                this.voiceService = value;
                this.VoiceSelector.Items.Clear();
                var service = speechSynthesisServices.First(x => Regex.Replace(x.GetType().Name, "[A-Z]", " $0").Trim() == value);
            }
        }

        public string DefaultVoice { get; private set; }

        public Settings(IEnumerable<ISpeechSynthesisService> services)
        {
            this.InitializeComponent();
            foreach (var voice in services)
            {
                this.VoiceServices.Items.Add(Regex.Replace(voice.GetType().Name, "[A-Z]", " $0").Trim());
            }
            this.speechSynthesisServices = services.ToArray();
            this.VoiceService = this.VoiceServices.Items.First() as string;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            //this.URL = this.URLTextBox.Text;
            this.Hide();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
