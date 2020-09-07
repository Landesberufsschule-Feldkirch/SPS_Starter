using SPS_Starter.Commands;
using System.Windows.Input;

namespace SPS_Starter.ViewModel
{
    public class ViewModel
    {
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mw)
        {
            _mainWindow = mw;
            ViAnzeige = new VisuAnzeigen();
        }

        
        private ICommand _tabControlSelectionChanged;
        // ReSharper disable once UnusedMember.Global
        public ICommand TabControlSelectionChanged => _tabControlSelectionChanged ?? (_tabControlSelectionChanged = new RelayCommand(_mainWindow.TabControlSelectionChanged));

        

        private ICommand _btnHaken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnHaken => _btnHaken ?? (_btnHaken = new RelayCommand(_mainWindow.ButtonGeaendert));

        private ICommand _btnProjektStarten;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnProjektStarten => _btnProjektStarten ?? (_btnProjektStarten = new RelayCommand(_mainWindow.ProjektStarten));
        
        
    }
}