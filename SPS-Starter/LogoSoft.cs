using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_Logo8_Lesen()
        {
            ProjektbezeichnungenStackpanelLeerenAktualisieren(gEigenschaften_Logo8, gButton_Logo8);
            ProgrammiersprachenListeAktualisieren(gAlleProgrammierSprachen_Logo8, gEigenschaften_Logo8);
            AnzeigeAktualisieren(gEigenschaften_Logo8);           

            gAnzeige_Logo8_Aktualisieren = true;
        }               

        private void Logo8_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            gProjekt_Name = rb.Name;
            WebBrowserFuellen(gAlleProgrammierSprachen_Logo8, gButton_Logo8, gEigenschaften_Logo8);
        }
    }
}