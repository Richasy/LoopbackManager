using LoopbackManager.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LoopbackManager.ViewModels
{
    public class LoopbackViewModel:INotifyPropertyChanged
    {
        private LoopbackController _loopbackController;
        public ObservableCollection<AppContainer> DisplayAppCollection;

        public static LoopbackViewModel Instance = new Lazy<LoopbackViewModel>(new LoopbackViewModel()).Value;

        private bool _hasUnsavedData;

        /// <summary>
        /// Has unsaved data.
        /// </summary>
        public bool HasUnsavedData
        {
            get => _hasUnsavedData;
            set
            {
                _hasUnsavedData = value;
                OnPropertyChanged();
            }
        }

        public LoopbackViewModel()
        {
            _loopbackController = new LoopbackController();
            DisplayAppCollection = new ObservableCollection<AppContainer>();
        }

        /// <summary>
        /// Load application list.
        /// </summary>
        /// <param name="keyword"></param>
        public void LoadApps(string keyword = "")
        {
            DisplayAppCollection.Clear();
            _loopbackController.LoadApps();
            FilterAppList(keyword);
        }

        /// <summary>
        /// Filter the app list.
        /// </summary>
        /// <param name="keyword"></param>
        public void FilterAppList(string keyword)
        {
            DisplayAppCollection.Clear();
            if (!string.IsNullOrEmpty(keyword))
            {
                _loopbackController.Apps.Where(p => p.DisplayName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .ForEach(p => DisplayAppCollection.Add(p));
            }
            else
            {
                _loopbackController.Apps.ForEach(p => DisplayAppCollection.Add(p));
            }
        }

        /// <summary>
        /// Save loopback state.
        /// </summary>
        public void SaveLoopbackState()
        {
            _loopbackController.SaveLoopbackState();
        }

        public void Dispose()
        {
            _loopbackController.FreeResources();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
