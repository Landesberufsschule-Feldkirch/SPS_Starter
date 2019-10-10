using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_TwinCAT_Lesen()
        {
            ProjektbezeichnungenStackpanelLeerenAktualisieren(gEigenschaften_TwinCAT, gButton_TwinCAT);
            ProgrammiersprachenListeAktualisieren(gAlleProgrammierSprachen_TwinCAT, gEigenschaften_TwinCAT);
            AnzeigeAktualisieren(gEigenschaften_TwinCAT);

            gAnzeige_TwinCAT_Aktualisieren = true;
        }
            private void TwinCAT_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            gProjekt_Name = rb.Name;
            WebBrowserFuellen( gAlleProgrammierSprachen_TwinCAT, gEigenschaften_TwinCAT);
        }
    }
}