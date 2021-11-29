using LoopbackManager.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
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

        private AppWindowTitleBar AppWindowTitleBar;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "Loopback Manager";
            this.Closed += OnWindowClosed;

            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
                AppWindowTitleBar = appWindow.TitleBar;
                AppWindowTitleBar.ExtendsContentIntoTitleBar = true;
                AppWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                AppWindowTitleBar.ButtonBackgroundColor = Colors.Transparent;
                TitleIcon.Margin = new Thickness(AppWindowTitleBar.LeftInset + 12, 0, 8, 0);
                TitleText.Margin = new Thickness(0, 0,AppWindowTitleBar.RightInset,0);
                TitleBarArea.SizeChanged += TitleBarArea_SizeChanged;
                TitleBarArea.Visibility = Visibility.Visible;
            }
        }

        private void TitleBarArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AppWindowTitleBar != null)
            {
                AppWindowTitleBar.SetDragRectangles(new Windows.Graphics.RectInt32[] { new Windows.Graphics.RectInt32()
                    {
                        Width = (int) e.NewSize.Width,
                        Height = (int)e.NewSize.Height
                    }});
            }
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
