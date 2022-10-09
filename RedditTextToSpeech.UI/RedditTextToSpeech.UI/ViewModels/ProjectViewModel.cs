using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
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
    public class ProjectViewModel : ObservableObject, IProject
    {
        public ProjectViewModel(IEventManager eventHandler)
        {
            this.Entries = new ObservableCollection<IEntry>();
            this.Header = "New Project";

            this.PushEmptyBackCommand = new RelayCommand(PushEmptyBack);
            this.PushEmptyFrontCommand = new RelayCommand(PushEmptyFront);

            eventHandler.Subscribe<EntryViewModel>("MoveUp", MoveUp);
            eventHandler.Subscribe<EntryViewModel>("MoveDown", MoveDown);
            eventHandler.Subscribe<EntryViewModel>("DeleteEntry", DeleteEntry);
        }

        public ProjectViewModel() : this(Ioc.Default.GetService<IEventManager>()) { }

        private string header;
        public string Header
        {
            get => this.header;
            set => this.SetProperty(ref this.header, value);
        }

        public ICommand PushEmptyBackCommand { get; }
        public ICommand PushEmptyFrontCommand { get; }

        [XmlElement]
        public IList<IEntry> Entries { get; }

        public void AddEntry(EntryViewModel entry)
        {
            this.Entries.Add(entry);
        }

        public void PushEmptyBack()
        {
            this.Entries.Add(Ioc.Default.GetRequiredService<EntryViewModel>());
        }

        public void PushEmptyFront()
        {
            this.Entries.Insert(0, Ioc.Default.GetRequiredService<EntryViewModel>());
        }

        public void MoveUp(EntryViewModel entry)
        {
            if (entry == null || !this.Entries.Contains(entry)) return;
            var index = this.Entries.IndexOf(entry);
            if (index - 1 < 0) return;
            this.Entries.Remove(entry);
            this.Entries.Insert(index - 1, entry);
        }
        public void MoveDown(EntryViewModel entry)
        {
            if (entry == null || !this.Entries.Contains(entry)) return;
            var index = this.Entries.IndexOf(entry);
            if (index + 1 > this.Entries.Count - 1) return;
            this.Entries.Remove(entry);
            this.Entries.Insert(index + 1, entry);
        }
        public void DeleteEntry(EntryViewModel entry)
        {
            if (entry == null || !this.Entries.Contains(entry)) return;
            this.Entries.Remove(entry);
        }
    }
}