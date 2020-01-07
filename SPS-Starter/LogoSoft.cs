using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public class LogoSoft : SharedDisplay
    {
        public bool AnzeigeAktualisieren { get; set; }

        public List<Button> ButtonListe = new List<Button>(); // { get; set; }


        readonly MainWindow MainWindow;
        readonly List<AlleEigenschaften> AlleEigenschaften;
        readonly List<AlleProgrammierSprachen> AlleProgrammierSprachen = new List<AlleProgrammierSprachen>();
        readonly Logo Logo;
        readonly string ButtonBeschriftung;

        public string OrdnerQuelle { get; set; }
        public string OrdnerZiel { get; set; }

        public LogoSoft(MainWindow mainWindow, List<AlleEigenschaften> alleEigenschaften, List<AlleProgrammierSprachen> alleProgrammierSprachen, Logo logo, string btnBeschriftung)
        {
            this.MainWindow = mainWindow;
            AlleEigenschaften = alleEigenschaften;
            AlleProgrammierSprachen = alleProgrammierSprachen;
            Logo = logo;
            ButtonBeschriftung = btnBeschriftung;
            AnzeigeAktualisieren = true;

            OrdnerQuelle = Logo.Source;
            OrdnerZiel = Logo.Destination;
        }

        public void ProjekteLesen()
        {
            StackpanelAktualisieren(AlleEigenschaften, ButtonListe);
            ProgrammiersprachenAktualisieren(AlleProgrammierSprachen, AlleEigenschaften, Logo.Source);
            AnzeigenAktualisieren(this.MainWindow, AlleEigenschaften);

            AnzeigeAktualisieren = true;
        }

        public void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            this.MainWindow.gProjekt_Name = rb.Name;
            HtmlFeldFuellen(this.MainWindow, AlleProgrammierSprachen, AlleEigenschaften, ButtonListe, Logo.Source, ButtonBeschriftung);
        }
    }
}