using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_Logo8_Lesen()
        {
            ProjektbezeichnungenStackpanelLeerenAktualisieren(Eigenschaften_Logo8, Button_Logo8_Liste);
            ProgrammiersprachenListeAktualisieren(ProjektOrdner_Logo8_Quelle, AlleProgrammierSprachen_Logo8, Eigenschaften_Logo8);
            AnzeigeAktualisieren(Eigenschaften_Logo8);           

            Anzeige_Logo8_Aktualisieren = true;
        }               

        private void Logo8_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            WebBrowserFuellen("Logo Projekt starten", ProjektOrdner_Logo8_Quelle, rb.Name, Eigenschaften_Logo8);
        }
    }
}