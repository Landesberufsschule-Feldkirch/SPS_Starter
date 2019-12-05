using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_TiaPortal_Lesen()
        {
            ProjektbezeichnungenStackpanelLeerenAktualisieren(gEigenschaften_TiaPortal, gButton_TiaPortal);
            ProgrammiersprachenListeAktualisieren(gAlleProgrammierSprachen_TiaPortal, gEigenschaften_TiaPortal);
            AnzeigeAktualisieren(gEigenschaften_TiaPortal);

            gAnzeige_TiaPortal_Aktualisieren = true;
        }

        private void TiaPortal_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            gProjekt_Name = rb.Name;
            WebBrowserFuellen(gAlleProgrammierSprachen_TiaPortal, gButton_TiaPortal, gEigenschaften_TiaPortal);
        }
    }
}