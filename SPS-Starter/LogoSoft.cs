using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public class LogoSoft : SharedDisplay
    {
        public bool AnzeigeAktualisieren { get; set; }
        public string OrdnerQuelle { get; set; }
        public string OrdnerZiel { get; set; }

        public List<Button> ButtonListe = new List<Button>();
        readonly MainWindow mW;
        readonly List<AlleEigenschaften> AlleEigenschaften = new List<AlleEigenschaften>();
        readonly List<AlleProgrammierSprachen> AlleProgrammierSprachen = new List<AlleProgrammierSprachen>();
        readonly Logo Logo;
        readonly string ButtonBeschriftung = "Logo Projekt starten";

        public LogoSoft(MainWindow mainWindow, Logo logo)
        {
            mW = mainWindow;
            Logo = logo;
            AnzeigeAktualisieren = true;
            OrdnerQuelle = Logo.Source;
            OrdnerZiel = Logo.Destination;

            AlleEigenschaften.Add(new AlleEigenschaften("PLC", "Logo8", mW.Web_Logo8_PLC, mW.StackPanel_Logo8_PLC, mW.Button_Starten_Logo8_PLC));
            AlleEigenschaften.Add(new AlleEigenschaften("BUG", "Logo8", mW.Web_Logo8_PLC_Bugs, mW.StackPanel_Logo8_PLC_Bugs, mW.Button_Starten_Logo8_PLC_Bugs));

            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("FUP", 4, mW.Checkbox_Logo8_FUP));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("KOP", 4, mW.Checkbox_Logo8_KOP));
        }

        public void ProjekteLesen()
        {
            StackpanelAktualisieren(AlleEigenschaften, ButtonListe);
            ProgrammiersprachenAktualisieren(AlleProgrammierSprachen, AlleEigenschaften, Logo.Source);
            AnzeigenAktualisieren(this.mW, AlleEigenschaften);

            AnzeigeAktualisieren = true;
        }

        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            mW.gProjekt_Name = rb.Name;
            HtmlFeldFuellen(mW, AlleProgrammierSprachen, AlleEigenschaften, ButtonListe, Logo.Source, ButtonBeschriftung);
        }
    }
}