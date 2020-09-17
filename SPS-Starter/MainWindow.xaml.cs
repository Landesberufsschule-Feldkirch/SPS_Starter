using SPS_Starter.Model;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public AlleDaten AlleDaten { get; set; }
        public AlleWerte AlleWerte { get; set; }
        public ProjektEigenschaften AktuellesProjekt { get; set; }
        public SpsStarter.Steuerungen AktuelleSteuerung { get; set; }

        private readonly ViewModel.ViewModel _viewModel;

        public MainWindow()
        {
            AktuelleSteuerung = SpsStarter.Steuerungen.Logo;
            AlleWerte = new AlleWerte();

            _viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = _viewModel;

            AlleDaten = new AlleDaten(this);
            
            AnzeigeUpdaten(SpsStarter.Steuerungen.Logo);
            AnzeigeUpdaten(SpsStarter.Steuerungen.TwinCat);
            AnzeigeUpdaten(SpsStarter.Steuerungen.TiaPortal);
        }
    }
}