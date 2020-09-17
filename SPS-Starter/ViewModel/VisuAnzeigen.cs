using System.ComponentModel;

namespace SPS_Starter.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            StartButtonFarbe = "LightGray";
            StartButtonInhalt = "Bitte ein Projekt auswählen";
        }

        private string _startButtonFarbe;
        public string StartButtonFarbe
        {
            get => _startButtonFarbe;
            set
            {
                _startButtonFarbe = value;
                OnPropertyChanged(nameof(StartButtonFarbe));
            }
        }

        private string _startButtonInhalt;
        public string StartButtonInhalt
        {
            get => _startButtonInhalt;
            set
            {
                _startButtonInhalt = value;
                OnPropertyChanged(nameof(StartButtonInhalt));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}