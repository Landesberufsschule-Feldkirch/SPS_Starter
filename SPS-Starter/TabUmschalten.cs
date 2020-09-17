using System;
using System.Windows.Controls;
using SPS_Starter.Model;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is TabControl tabControl) || !(tabControl.SelectedValue is TabItem item)) return;

            _viewModel.ViAnzeige.StartButtonInhalt = "Bitte ein Projekt auswählen";
            _viewModel.ViAnzeige.StartButtonFarbe = "LightGray";

            HtmlFensterLoeschen();

            AktuelleSteuerung = item.Header.ToString() switch
            {
                "Logo8" => SpsStarter.Steuerungen.Logo,
                "TiaPortal" => SpsStarter.Steuerungen.TiaPortal,
                "TwinCAT" => SpsStarter.Steuerungen.TwinCat,
                _ => AktuelleSteuerung
            };
            AnzeigeUpdaten(AktuelleSteuerung);
        }

        private void HtmlFensterLoeschen()
        {
            foreach (var tabEigenschaften in AlleDaten.AlleTabEigenschaften)
            {
                tabEigenschaften.BrowserBezeichnung.Navigate((Uri)null);
            }
        }
    }
}