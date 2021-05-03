using LoopbackManager.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LoopbackManager.Controls
{
    public sealed partial class AppItem : UserControl
    {
        public AppItem()
        {
            this.InitializeComponent();
        }

        public event EventHandler StateChanged;

        public AppContainer Data
        {
            get { return (AppContainer)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(AppContainer), typeof(AppItem), new PropertyMetadata(null, new PropertyChangedCallback(OnDataChanged)));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as AppItem;
            if (e.NewValue != null && e.NewValue is AppContainer data)
            {
                string path= string.IsNullOrEmpty(data.WorkingDirectory) ? "--" : data.WorkingDirectory;
                instance.AppNameBlock.Text = data.DisplayName;
                instance.AppPathBlock.Text = path;
                ToolTipService.SetToolTip(instance.AppNameBlock, data.DisplayName);
                ToolTipService.SetToolTip(instance.AppPathBlock, path);
            }
        }

        private void SelectedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
