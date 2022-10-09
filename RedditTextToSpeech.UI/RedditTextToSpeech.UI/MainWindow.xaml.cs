using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RedditTextToSpeech.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace RedditTextToSpeech.UI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static MainWindow instance;

        public MainWindow()
        {
            this.InitializeComponent();
            instance = this;
            this._main.DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
        }

        /// <summary>
        /// ViewModel for the window's grid, acting as the MainWindow's view model.
        /// </summary>
        public MainWindowViewModel ViewModel => (MainWindowViewModel)this._main.DataContext;

        /// <summary>
        /// Used to reset the xaml root.
        /// </summary>
        public static XamlRoot ResetXamlRoot => instance._main.XamlRoot;

        /// <summary>
        /// Wrap around for tab closing not being supported as a command.
        /// </summary>
        private void TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            if (args.Item is ProjectViewModel project)
            {
                this.ViewModel.CloseProject(project);
            }
        }
    }
}