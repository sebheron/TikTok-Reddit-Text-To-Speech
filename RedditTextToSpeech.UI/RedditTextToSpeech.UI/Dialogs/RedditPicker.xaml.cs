using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RedditTextToSpeech.Logic.Containers;
using System.Collections.Generic;

namespace RedditTextToSpeech.UI.Dialogs
{
    public sealed partial class RedditPicker : ContentDialog
    {
        public string URL { get; private set; }

        public RedditPicker()
        {
            this.InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            this.URL = this.URLTextBox.Text;
            this.Hide();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
