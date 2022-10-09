using RedditTextToSpeech.Core.Content;
using RedditTextToSpeech.Logic.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace RedditTextToSpeech.UI.Containers
{
    public class OfflineVoice : IVoice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfflineVoice"/> class.
        /// </summary>
        /// <param name="voice"></param>
        public OfflineVoice(VoiceInformation voice)
        {
            this.Voice = voice;
        }

        /// <summary>
        /// The installed windows voice.
        /// </summary>
        public VoiceInformation Voice { get; }

        /// <summary>
        /// The display name for the voice.
        /// </summary>
        public string DisplayName => this.Voice.DisplayName;

        /// <summary>
        /// The system name for the voice.
        /// </summary>
        public string Name => this.Voice.Id;

        /// <summary>
        /// The gender of the voice.
        /// </summary>
        public Gender Gender
        {
            get
            {
                switch (this.Voice.Gender)
                {
                    case VoiceGender.Male: return Gender.Male;
                    case VoiceGender.Female: return Gender.Female;
                    default: return Gender.Unknown;
                }
            }
        }

        /// <summary>
        /// Is the voice a special voice or not?
        /// </summary>
        public bool SpecialVoice => false;
    }
}
