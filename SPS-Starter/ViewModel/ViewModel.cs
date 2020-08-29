using System.Windows.Input;
using SPS_Starter.Commands;
using SPS_Starter.Model;

namespace SPS_Starter.ViewModel
{
    public class ViewModel
    {
        private readonly MainWindow _mainWindow;
        private readonly Model.SpsStarter _spsStarter;
        public Model.SpsStarter SpsStarter => _spsStarter;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mw)
        {
            _mainWindow = mw;
            var spsStarter = new Model.SpsStarter();
            ViAnzeige = new VisuAnzeigen(_mainWindow, spsStarter);
        }

        

        private ICommand _tabControlSelectionChanged;
        // ReSharper disable once UnusedMember.Global
        public ICommand TabControlSelectionChanged => _tabControlSelectionChanged ?? (_tabControlSelectionChanged = new RelayCommand(_mainWindow.TabControlSelectionChanged));



        private ICommand _btnProjektStarten;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnProjektStarten => _btnProjektStarten ?? (_btnProjektStarten = new RelayCommand(_mainWindow.ProjektStarten));
        
        
    }
}