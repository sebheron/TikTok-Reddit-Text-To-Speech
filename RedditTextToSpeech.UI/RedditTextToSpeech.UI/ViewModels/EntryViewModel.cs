using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using RedditTextToSpeech.Core.Interfaces;
using RedditTextToSpeech.Logic.Containers;
using RedditTextToSpeech.Logic.Services;
using RedditTextToSpeech.UI.Enums;
using RedditTextToSpeech.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace RedditTextToSpeech.UI.ViewModels
{
    public class EntryViewModel : ObservableObject, IEntry
    {
        private IEventManager eventHandler;

        public EntryViewModel(IEventManager eventHandler, ISpeechSynthesisService speechSynthesisService)
        {
            this.eventHandler = eventHandler;

            this.Voices = new ObservableCollection<IVoice>(speechSynthesisService.Voices);
            this.SelectedVoice = this.Voices.FirstOrDefault();

            this.ShouldUse = true;
            
            this.MoveUpCommand = new RelayCommand(MoveUp);
            this.MoveDownCommand = new RelayCommand(MoveDown);
            this.DeleteEntryCommand = new RelayCommand(DeleteEntry);
        }

        public EntryViewModel() : this(Ioc.Default.GetService<IEventManager>(), Ioc.Default.GetService<ISpeechSynthesisService>()) { }

        [XmlAttribute]
        private bool shouldUse;
        public bool ShouldUse
        {
            get => this.shouldUse;
            set => this.SetProperty(ref this.shouldUse, value);
        }

        [XmlAttribute]
        private EntryType selectedEntryType;
        public EntryType SelectedEntryType
        {
            get => this.selectedEntryType;
            set
            {
                this.SetProperty(ref this.selectedEntryType, value);
                this.OnPropertyChanged(nameof(this.TextFontSize));
            }
        }

        [XmlAttribute]
        private IVoice selectedVoice;
        public IVoice SelectedVoice
        {
            get => this.selectedVoice;
            set => this.SetProperty(ref this.selectedVoice, value);
        }

        [XmlAttribute]
        private string commenterName;
        public string CommenterName
        {
            get => this.commenterName;
            set => this.SetProperty(ref this.commenterName, value);
        }

        [XmlAttribute]
        private string subredditName;
        public string SubredditName
        {
            get => this.subredditName;
            set => this.SetProperty(ref this.subredditName, value);
        }

        [XmlAttribute]
        private string text;
        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }

        public int TextFontSize
        {
            get
            {
                switch (this.selectedEntryType)
                {
                    case EntryType.Title:
                        return 30;
                    case EntryType.Comment:
                        return 15;
                    default:
                        return 15;
                }
            }
        }

        public EntryType[] EntryTypes => Enum.GetValues(typeof(EntryType)).Cast<EntryType>().ToArray();
        public ObservableCollection<IVoice> Voices { get; }

        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand DeleteEntryCommand { get; }

        public void MoveUp()
        {
            this.eventHandler.Emit<EntryViewModel>("MoveUp", this);
        }
        public void MoveDown()
        {
            this.eventHandler.Emit<EntryViewModel>("MoveDown", this);
        }
        public void DeleteEntry()
        {
            this.eventHandler.Emit<EntryViewModel>("DeleteEntry", this);
        }
    }
}
