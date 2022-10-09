using RedditTextToSpeech.Core.Content;

namespace RedditTextToSpeech.Logic.Containers
{
    public class DefinedVoice : IVoice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinedVoice"/> class.
        /// </summary>
        /// <param name="displayName">The display name for the voice.</param>
        /// <param name="name">The system name for the voice.</param>
        /// <param name="gender">The gender of the voice.</param>
        public DefinedVoice(string displayName, string name, Gender gender, bool specialVoice = false)
        {
            this.DisplayName = displayName;
            this.Name = name;
            this.Gender = gender;
            this.SpecialVoice = specialVoice;
        }

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
