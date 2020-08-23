using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public class TwinCat : SharedDisplay
    {
        public bool AnzeigeAktualisieren { get; set; }
        public string OrdnerQuelle { get; set; }
        public string OrdnerZiel { get; set; }

        public List<Button> ButtonListe = new List<Button>();
        readonly MainWindow mW;
        readonly List<AlleEigenschaften> AlleEigenschaften = new List<AlleEigenschaften>();
        readonly List<AlleProgrammierSprachen> AlleProgrammierSprachen = new List<AlleProgrammierSprachen>();
        readonly Logo Logo;
        readonly string ButtonBeschriftung = "TwinCAT Projekt starten";

        public TwinCat(MainWindow mainWindow, Logo logo)
        {
            mW = mainWindow;
            Logo = logo;
            AnzeigeAktualisieren = true;
            OrdnerQuelle = Logo.Source;
            OrdnerZiel = Logo.Destination;

            AlleEigenschaften.Add(new AlleEigenschaften("PLC", "TwinCAT", mW.WebTwinCatPlc, mW.StackPanelTwinCatPlc, mW.ButtonStartenTwinCatPlc));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_VISU", "TwinCAT", mW.WebTwinCatPlcVisu, mW.StackPanelTwinCatPlcVisu, mW.ButtonStartenTwinCatPlcVisu));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_NC", "TwinCAT", mW.WebTwinCatPlcNc, mW.StackPanelTwinCatPlcNc, mW.ButtonStartenTwinCatPlcNc));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_DT", "TwinCAT", mW.WebTwinCatPlcDt, mW.StackPanelTwinCatPlcDt, mW.ButtonStartenTwinCatPlcDt));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_ADS", "TwinCAT", mW.WebTwinCatPlcAds, mW.StackPanelTwinCatPlcAds, mW.ButtonStartenTwinCatPlcAds));
            AlleEigenschaften.Add(new AlleEigenschaften("PLC_BUG", "TwinCAT", mW.WebTwinCatPlcBugs, mW.StackPanelTwinCatPlcBugs, mW.ButtonStartenTwinCatPlcBugs));

            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("AS", 3, mW.CheckboxTwinCatAs));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("AWL", 4, mW.CheckboxTwinCatAwl));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("CFC", 4, mW.CheckboxTwinCatCfc));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("CPP", 4, mW.CheckboxTwinCatCpp));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("FUP", 4, mW.CheckboxTwinCatFup));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("KOP", 4, mW.CheckboxTwinCatKop));
            AlleProgrammierSprachen.Add(new AlleProgrammierSprachen("ST", 3, mW.CheckboxTwinCatSt));
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