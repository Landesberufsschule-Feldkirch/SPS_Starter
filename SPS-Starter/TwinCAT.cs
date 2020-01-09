using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public class TwinCAT : SharedDisplay
    {
        public bool AnzeigeAktualisieren { get; set; }
        public string OrdnerQuelle { get; set; }
        public string OrdnerZiel { get; set; }

        public List<Button> ButtonListe = new List<Button>();
        readonly MainWindow mW;
        readonly List<AlleEigenschaften> AlleEigenschaften = new List<AlleEigenschaften>();
        readonly List<AlleProgrammierSprachen> AlleProgrammierSprachen = new List<AlleProgrammierSprachen>();
        readonly Logo Logo;
        string ButtonBeschriftung = "TwinCAT Projekt starten";

        public TwinCAT(MainWindow mainWindow, Logo logo)
        {
            mW = mainWindow;
            Logo = logo;
            AnzeigeAktualisieren = true;
            OrdnerQuelle = Logo.Source;
            OrdnerZiel = Logo.Destination;

            AlleEigenschaften.Add(new AlleEigenschaften("PLC", "TwinCAT", mW.Web_TwinCAT_PLC, mW.StackPanel_TwinCAT_PLC, mW.Button_Starten_TwinCAT_PLC));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_VISU", "TwinCAT", mW.Web_TwinCAT_PLC_VISU, mW.StackPanel_TwinCAT_PLC_VISU, mW.Button_Starten_TwinCAT_PLC_VISU));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_NC", "TwinCAT", mW.Web_TwinCAT_PLC_NC, mW.StackPanel_TwinCAT_PLC_NC, mW.Button_Starten_TwinCAT_PLC_NC));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_DT", "TwinCAT", mW.Web_TwinCAT_PLC_DT, mW.StackPanel_TwinCAT_PLC_DT, mW.Button_Starten_TwinCAT_PLC_DT));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_BUG", "TwinCAT", mW.Web_TwinCAT_PLC_Bugs, mW.StackPanel_TwinCAT_PLC_Bugs, mW.Button_Starten_TwinCAT_PLC_Bugs));

            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("AS", 3, mW.Checkbox_TwinCAT_AS));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("AWL", 4, mW.Checkbox_TwinCAT_AWL));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("CFC", 4, mW.Checkbox_TwinCAT_CFC));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("CPP", 4, mW.Checkbox_TwinCAT_CPP));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("FUP", 4, mW.Checkbox_TwinCAT_FUP));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("KOP", 4, mW.Checkbox_TwinCAT_KOP));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("ST", 3, mW.Checkbox_TwinCAT_ST));
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