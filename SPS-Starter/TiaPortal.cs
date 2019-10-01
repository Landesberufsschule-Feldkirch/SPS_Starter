using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_TiaPortal_Lesen()
        {
            ProjektbezeichnungenStackpanelLeerenAktualisieren(Eigenschaften_TiaPortal, Button_TiaPortal_Liste);
            ProgrammiersprachenListeAktualisieren(ProjektOrdner_TiaPortal_Quelle, AlleProgrammierSprachen_TiaPortal, Eigenschaften_TiaPortal);
            AnzeigeAktualisieren(Eigenschaften_TiaPortal);

            Anzeige_TiaPortal_Aktualisieren = true;
        }

        private void TiaPortal_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            WebBrowserFuellen(ProjektOrdner_TiaPortal_Quelle, rb.Name, Eigenschaften_TiaPortal);
        }
    }

}