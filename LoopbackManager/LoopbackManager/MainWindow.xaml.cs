using LoopbackManager.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LoopbackManager
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public LoopbackViewModel ViewModel = LoopbackViewModel.Instance;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "Loopback Manager";
            this.Closed += OnWindowClosed;
        }

        private void OnWindowClosed(object sender, WindowEventArgs args)
        {
            ViewModel.Dispose();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveLoopbackState();
            ViewModel.HasUnsavedData = false;
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            bool needClear = !string.IsNullOrEmpty(KeywordBox.Text);
            if (needClear)
            {
                KeywordBox.Text = string.Empty;
            }
            else
            {
                ViewModel.LoadApps();
            }
        }

        private void OnListViewLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadApps();
        }

        private void OnKeywordChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = KeywordBox.Text;
            ViewModel.FilterAppList(keyword);
        }

        private void OnItemStatusChanged(object sender, EventArgs e)
        {
            ViewModel.HasUnsavedData = true;
        }
    }
}
