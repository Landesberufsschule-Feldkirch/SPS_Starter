using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public class TiaPortal : SharedDisplay
    {
        public bool AnzeigeAktualisieren { get; set; }
        public string OrdnerQuelle { get; set; }
        public string OrdnerZiel { get; set; }

        public List<Button> ButtonListe = new List<Button>();
        readonly MainWindow mW;
        readonly List<AlleEigenschaften> AlleEigenschaften = new List<AlleEigenschaften>();
        readonly List<AlleProgrammierSprachen> AlleProgrammierSprachen = new List<AlleProgrammierSprachen>();
        readonly Logo Logo;
        readonly string ButtonBeschriftung = "TiaPortal Projekt starten";

        public TiaPortal(MainWindow mainWindow, Logo logo)
        {
            mW = mainWindow;
            Logo = logo;
            AnzeigeAktualisieren = true;
            OrdnerQuelle = Logo.Source;
            OrdnerZiel = Logo.Destination;

            AlleEigenschaften.Add(new AlleEigenschaften("PLC", "TIA_PORTAL_V14_SP1", mW.Web_TiaPortal_PLC, mW.StackPanel_TiaPortal_PLC, mW.Button_Starten_TiaPortal_PLC));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_HMI", "TIA_PORTAL_V14_SP1", mW.Web_TiaPortal_PLC_HMI, mW.StackPanel_TiaPortal_PLC_HMI, mW.Button_Starten_TiaPortal_PLC_HMI));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_FIO", "TIA_PORTAL_V14_SP1", mW.Web_TiaPortal_PLC_FIO, mW.StackPanel_TiaPortal_PLC_FIO, mW.Button_Starten_TiaPortal_PLC_FIO));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_DT", "TIA_PORTAL_V14_SP1", mW.Web_TiaPortal_PLC_DT, mW.StackPanel_TiaPortal_PLC_DT, mW.Button_Starten_TiaPortal_PLC_DT));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_Snap7", "TIA_PORTAL_V14_SP1", mW.Web_TiaPortal_PLC_Snap7, mW.StackPanel_TiaPortal_PLC_Snap7, mW.Button_Starten_TiaPortal_PLC_Snap7));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_BUG", "TIA_PORTAL_V14_SP1", mW.Web_TiaPortal_PLC_Bugs, mW.StackPanel_TiaPortal_PLC_Bugs, mW.Button_Starten_TiaPortal_PLC_Bugs));

            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("FUP", 4, mW.Checkbox_TiaPortal_FUP));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("KOP", 4, mW.Checkbox_TiaPortal_KOP));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("SCL", 4, mW.Checkbox_TiaPortal_SCL));
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