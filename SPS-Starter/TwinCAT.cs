using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_TwinCAT_Lesen()
        {
            ProjektbezeichnungenStackpanelLeerenAktualisieren(Eigenschaften_TwinCAT, Button_TwinCAT_Liste);
            ProgrammiersprachenListeAktualisieren(ProjektOrdner_TwinCAT_Quelle, AlleProgrammierSprachen_TwinCAT, Eigenschaften_TwinCAT);
            AnzeigeAktualisieren(Eigenschaften_TwinCAT);

            Anzeige_TwinCAT_Aktualisieren = true;
        }

            private void TwinCAT_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            WebBrowserFuellen(ProjektOrdner_TwinCAT_Quelle, rb.Name, Eigenschaften_TwinCAT);
        }

    }
}