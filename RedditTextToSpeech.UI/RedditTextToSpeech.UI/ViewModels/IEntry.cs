using RedditTextToSpeech.Logic.Containers;
using RedditTextToSpeech.UI.Enums;

namespace RedditTextToSpeech.UI.ViewModels
{
    public interface IEntry
    {
        public bool ShouldUse { get; set; }

        public EntryType SelectedEntryType { get; set; }

        public IVoice SelectedVoice { get; set; }

        public string CommenterName { get; set; }

        public string SubredditName { get; set; }

        public string Text { get; set; }
    }
}
