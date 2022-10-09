using RedditTextToSpeech.Core.Content;
using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Text;

namespace RedditTextToSpeech.Logic.Containers
{
    public class WindowsVoice : IVoice
    {
        /// <summary>
        /// The installed windows voice.
        /// </summary>
        private InstalledVoice voice;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsVoice"/> class.
        /// </summary>
        /// <param name="voice"></param>
        public WindowsVoice(InstalledVoice voice)
        {
            this.voice = voice;
        }

        /// <summary>
        /// The display name for the voice.
        /// </summary>
        public string DisplayName => this.voice.VoiceInfo.Description;

        /// <summary>
        /// The system name for the voice.
        /// </summary>
        public string Name => this.voice.VoiceInfo.Name;

        /// <summary>
        /// The gender of the voice.
        /// </summary>
        public Gender Gender { 
            get
            {
                switch (this.voice.VoiceInfo.Gender)
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
