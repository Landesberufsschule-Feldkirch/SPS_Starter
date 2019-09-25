using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;

namespace SPS_Starter
{
    public class AlleEigenschaften
    {
        public string Kurzbezeichnung { get; set; }
        public string GruppenName { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public StackPanel StackPanelBezeichnung { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public List<Tuple<string, string, string>> ProjekteBezeichnung { get; set; }

        public AlleEigenschaften(string Kurzbezeichnung, string GruppenName, WebBrowser BrowserBezeichnung, StackPanel StackPanelBezeichnung, Button ButtonBezeichnung)
        {
            this.Kurzbezeichnung = Kurzbezeichnung;
            this.GruppenName = GruppenName;
            this.BrowserBezeichnung = BrowserBezeichnung;
            this.StackPanelBezeichnung = StackPanelBezeichnung;
            this.ButtonBezeichnung = ButtonBezeichnung;
            this.ProjekteBezeichnung = new List<Tuple<string, string, string>>();
        }
    }

    public class AlleProgrammierSprachen
    {
        public string Kurzbezeichnung { get; set; }
        public int Laenge { get; set; }
        public CheckBox CheckBoxBezeichnung { get; set; }

        public AlleProgrammierSprachen(string Kurzbezeichnung, int Laenge, CheckBox CheckBoxBezeichnung)
        {
            this.Kurzbezeichnung = Kurzbezeichnung;
            this.Laenge = Laenge;
            this.CheckBoxBezeichnung = CheckBoxBezeichnung;
        }
    }

    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            try
            {
                uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 1 caught.");
            }
        }
    }

    public partial class MainWindow : Window
    {
        ObservableCollection<AlleEigenschaften> Eigenschaften_Logo8 = new ObservableCollection<AlleEigenschaften>();
        ObservableCollection<AlleEigenschaften> Eigenschaften_TiaPortal = new ObservableCollection<AlleEigenschaften>();
        ObservableCollection<AlleEigenschaften> Eigenschaften_TwinCAT = new ObservableCollection<AlleEigenschaften>();

        ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen_Logo8 = new ObservableCollection<AlleProgrammierSprachen>();
        ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen_TiaPortal = new ObservableCollection<AlleProgrammierSprachen>();
        ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen_TwinCAT = new ObservableCollection<AlleProgrammierSprachen>();

        string LeereHtmlSeite = "<!doctype html>   </html >";
        string HtmlSeite = "";

        bool Anzeige_Logo8_Aktualisieren = false;
        bool Anzeige_TiaPortal_Aktualisieren = false;
        bool Anzeige_TwinCAT_Aktualisieren = false;

        string Projekt_Logo8_Name = "";
        string Projekt_TiaPortal_Name = "";
        string Projekt_TwinCAT_Name = "";

        string ProjektOrdner_Logo8_Quelle = "";
        string ProjektOrdner_TiaPortal_Quelle = "";
        string ProjektOrdner_TwinCAT_Quelle = "";

        string Projekt_Logo8_Ziel = "h:\\Logo8V81";
        string Projekt_TiaPortal_Ziel = "h:\\TiaPortal_V14";
        string Projekt_TwinCAT_Ziel = "h:\\TwinCAT_V3";

        List<Button> Button_Logo8_Liste = new List<Button>();
        List<Button> Button_TiaPortal_Liste = new List<Button>();
        List<Button> Button_TwinCAT_Liste = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            EinstellungenLesen("Einstellungen.xml");
            InstanzenFuellen();
            ProjekteLesen();
        }

        public void ProjekteLesen()
        {
            Projekte_Logo8_Lesen();
            Projekte_TiaPortal_Lesen();
            Projekte_TwinCAT_Lesen();
        }

        public void EinstellungenLesen(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            foreach (XElement el in doc.Root.Elements())
            {
                switch (el.Name.LocalName)
                {
                    case "Logo": ProjektOrdner_Logo8_Quelle = el.Value.Trim(); break;
                    case "TiaPortal": ProjektOrdner_TiaPortal_Quelle = el.Value.Trim(); break;
                    case "TwinCAT": ProjektOrdner_TwinCAT_Quelle = el.Value.Trim(); break;
                    default: break;
                }
            }
        }

        public void InstanzenFuellen()
        {
            Eigenschaften_Logo8.Add(new AlleEigenschaften("PLC", "Logo8!", Web_Logo8_PLC, StackPanel_Logo8_PLC, Button_Starten_Logo8_PLC));
            Eigenschaften_Logo8.Add(new AlleEigenschaften("BUG", "Logo8!", Web_Logo8_PLC_Bugs, StackPanel_Logo8_PLC_Bugs, Button_Starten_Logo8_PLC_Bugs));

            Eigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC", "TIA_PORTAL_V14_SP1", Web_TiaPortal_PLC, StackPanel_TiaPortal_PLC, Button_Starten_TiaPortal_PLC));
            Eigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_DT", "TIA_PORTAL_V14_SP1", Web_TwinCAT_PLC_DT, StackPanel_TiaPortal_PLC_DT, Button_Starten_TiaPortal_PLC_DT));
            Eigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_HMI", "TIA_PORTAL_V14_SP1", Web_TiaPortal_PLC_HMI, StackPanel_TiaPortal_PLC_HMI, Button_Starten_TiaPortal_PLC_HMI));
            Eigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_FIO", "TIA_PORTAL_V14_SP1", Web_TiaPortal_PLC_FIO, StackPanel_TiaPortal_PLC_FIO, Button_Starten_TiaPortal_PLC_FIO));
            Eigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_BUG", "TIA_PORTAL_V14_SP1", Web_TiaPortal_PLC_Bugs, StackPanel_TiaPortal_PLC_Bugs, Button_Starten_TiaPortal_PLC_Bugs));
            Eigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_Snap7", "TIA_PORTAL_V14_SP1", Web_TiaPortal_PLC_Snap7, StackPanel_TiaPortal_PLC_Snap7, Button_Starten_TiaPortal_PLC_Snap7));

            Eigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC", "TwinCAT", Web_TwinCAT_PLC, StackPanel_TwinCAT_PLC, Button_Starten_TwinCAT_PLC));
            Eigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_DT", "TwinCAT", Web_TwinCAT_PLC_DT, StackPanel_TwinCAT_PLC_DT, Button_Starten_TwinCAT_PLC_DT));
            Eigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_VISU", "TwinCAT", Web_TwinCAT_PLC_VISU, StackPanel_TwinCAT_PLC_VISU, Button_Starten_TwinCAT_PLC_VISU));
            Eigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_NC", "TwinCAT", Web_TwinCAT_PLC_NC, StackPanel_TwinCAT_PLC_NC, Button_Starten_TwinCAT_PLC_NC));
            Eigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_BUG", "TwinCAT", Web_TwinCAT_PLC_Bugs, StackPanel_TwinCAT_PLC_Bugs, Button_Starten_TwinCAT_PLC_Bugs));



            AlleProgrammierSprachen_Logo8.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_Logo8_FUP));
            AlleProgrammierSprachen_Logo8.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_Logo8_KOP));

            AlleProgrammierSprachen_TiaPortal.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_TiaPortal_FUP));
            AlleProgrammierSprachen_TiaPortal.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_TiaPortal_KOP));
            AlleProgrammierSprachen_TiaPortal.Add(new AlleProgrammierSprachen("SCL", 4, Checkbox_TiaPortal_SCL));

            AlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("AS", 3, Checkbox_TwinCAT_AS));
            AlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("AWL", 4, Checkbox_TwinCAT_AWL));
            AlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("CFC", 4, Checkbox_TwinCAT_CFC));
            AlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_TwinCAT_FUP));
            AlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_TwinCAT_KOP));
            AlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("ST", 3, Checkbox_TwinCAT_ST));
        }
    }
}
