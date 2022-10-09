using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using RedditTextToSpeech.Core.Content;
using RedditTextToSpeech.Logic.Containers;
using RedditTextToSpeech.Logic.Services;
using RedditTextToSpeech.UI.Dialogs;
using RedditTextToSpeech.UI.Enums;
using RedditTextToSpeech.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Windows.Foundation.Metadata;
using Windows.Storage.Pickers;
//using WinUIFileDialog;

namespace RedditTextToSpeech.UI.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private IRedditService redditService;
        private IVideoService videoService;
        private IImageService imageService;
        private ISpeechSynthesisService speechSynthesisService;
        private IVideoDownloadService videoDownloadService;

        public MainWindowViewModel(IRedditService redditService,
            IVideoService videoService,
            IImageService imageService,
            ISpeechSynthesisService speechSynthesisService,
            IVideoDownloadService videoDownloadService)
        {
            this.redditService = redditService;
            this.videoService = videoService;
            this.imageService = imageService;
            this.speechSynthesisService = speechSynthesisService;
            this.videoDownloadService = videoDownloadService;

            this.Projects = new ObservableCollection<ProjectViewModel>();
            this.Projects.Add(Ioc.Default.GetRequiredService<ProjectViewModel>());
            this.CurrentProject = this.Projects.FirstOrDefault();

            this.AddNewProjectCommand = new RelayCommand(() => AddProject());
            this.SaveProjectCommand = new RelayCommand(SaveProject);
            this.LoadProjectCommand = new AsyncRelayCommand(LoadProject);
            this.LoadRedditURLCommand = new AsyncRelayCommand(LoadRedditURL);
            this.RenderCurrentProjectCommand = new AsyncRelayCommand(RenderCurrentProject);
        }

        public ObservableCollection<ProjectViewModel> Projects { get; }

        private ProjectViewModel currentProject;
        public ProjectViewModel CurrentProject
        {
            get => this.currentProject;
            set => this.SetProperty(ref this.currentProject, value);
        }

        public ICommand AddNewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand LoadProjectCommand { get; }
        public ICommand LoadRedditURLCommand { get; }
        public ICommand RenderCurrentProjectCommand { get; }

        public void AddProject(ProjectViewModel project = null)
        {
            project = project ?? Ioc.Default.GetRequiredService<ProjectViewModel>();
            this.Projects.Add(project ?? Ioc.Default.GetRequiredService<ProjectViewModel>());
            this.CurrentProject = project;
        }

        public void CloseProject(ProjectViewModel project)
        {
            this.Projects.Remove(project);
        }

        public void SaveProject()
        {
            using (var writer = new StringWriter())
            {
                new XmlSerializer(typeof(ProjectViewModel)).Serialize(writer, this.CurrentProject);
                File.WriteAllText("C:/Users/Sebhe/Documents/test.xml", writer.ToString());
            }
        }

        public async Task LoadProject()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            picker.FileTypeFilter.Add(".rttsg");

            var file = await picker.PickSingleFileAsync();

            XmlSerializer serializer = new XmlSerializer(typeof(ProjectViewModel));
            using (var reader = new StringReader(File.ReadAllText(file.Path)))
            {
                this.AddProject((ProjectViewModel)serializer.Deserialize(reader));
            }
        }

        public async Task LoadRedditURL()
        {
            var dialog = new RedditPicker();
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialog.XamlRoot = MainWindow.ResetXamlRoot;
            }
            
            await dialog.ShowAsync();
            
            if (dialog.URL != null)
            {
                var post = this.redditService.GetPostInformation(dialog.URL);
                var title = this.BuildEntry(EntryType.Title, post.Title, post.Username, post.Subreddit);
                this.CurrentProject.AddEntry(title);
                foreach (var content in post.Content)
                {
                    var entry = this.BuildEntry(EntryType.Text, content, post.Username, post.Subreddit);
                    this.CurrentProject.AddEntry(entry);
                }
            }
        }

        public async Task RenderCurrentProject()
        {
            var dialog = new Render(videoService, imageService, speechSynthesisService, videoDownloadService);
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialog.XamlRoot = MainWindow.ResetXamlRoot;
            }

            dialog.ProjectsToRender.Add(this.CurrentProject);

            await dialog.ShowAsync();
        }

        public EntryViewModel BuildEntry(EntryType entryType, string text, string commenter, string subreddit)
        {
            var entry = Ioc.Default.GetRequiredService<EntryViewModel>();
            entry.SelectedEntryType = entryType;
            entry.Text = text;
            entry.CommenterName = commenter;
            entry.SubredditName = subreddit;
            return entry;
        }
    }
}
