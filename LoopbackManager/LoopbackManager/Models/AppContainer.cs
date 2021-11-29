using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LoopbackManager.Models
{
    public class AppContainer : INotifyPropertyChanged
    {
        private bool _isLoopback;
        public string AppContainerName { get; set; }
        public string DisplayName { get; set; }
        public string WorkingDirectory { get; set; }
        public string Sid { get; set; }
        public bool IsLoopback
        {
            get => _isLoopback;
            set
            {
                _isLoopback = value;
                OnPropertyChanged();
            }
        }

        public AppContainer(string appContainerName, string displayName, string workingDirectory, string sid)
        {
            this.AppContainerName = appContainerName;
            this.DisplayName = displayName;
            this.WorkingDirectory = workingDirectory;
            this.Sid = sid;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
