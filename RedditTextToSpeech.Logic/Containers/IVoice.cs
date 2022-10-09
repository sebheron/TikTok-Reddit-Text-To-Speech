using RedditTextToSpeech.Core.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditTextToSpeech.Logic.Containers
{
    public interface IVoice
    {
        /// <summary>
        /// The display name for the voice.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// The system name for the voice.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The gender of the voice.
        /// </summary>
        public Gender Gender { get; }

        /// <summary>
        /// Is the voice a special voice or not?
        /// </summary>
        public bool SpecialVoice { get; }
    }
}
