using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;

namespace SPS_Starter
{
    public class AlleEigenschaften
    {
        public string Kurzbezeichnung { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public StackPanel StackPanelBezeichnung { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public List<Tuple<string, string, string>> ProjekteBezeichnung { get; set; }

        public AlleEigenschaften(string Kurzbezeichnung, WebBrowser BrowserBezeichnung, StackPanel StackPanelBezeichnung, Button ButtonBezeichnung)
        {
            this.Kurzbezeichnung = Kurzbezeichnung;
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
        ObservableCollection<AlleProgrammierSprachen> Sprachen_TiaPortal = new ObservableCollection<AlleProgrammierSprachen>();
        ObservableCollection<AlleProgrammierSprachen> Sprachen_TwinCAT = new ObservableCollection<AlleProgrammierSprachen>();







        ObservableCollection<Tuple<string, WebBrowser>> Coll_Html_Logo8 = new ObservableCollection<Tuple<string, WebBrowser>>();
        ObservableCollection<Tuple<string, WebBrowser>> Coll_Html_TiaPortal = new ObservableCollection<Tuple<string, WebBrowser>>();
        ObservableCollection<Tuple<string, WebBrowser>> Coll_Html_TwinCAT = new ObservableCollection<Tuple<string, WebBrowser>>();

        ObservableCollection<Tuple<string, CheckBox, int>> Coll_Checked_Logo8 = new ObservableCollection<Tuple<string, CheckBox, int>>();
        ObservableCollection<Tuple<string, CheckBox, int>> Coll_Checked_TiaPortal = new ObservableCollection<Tuple<string, CheckBox, int>>();
        ObservableCollection<Tuple<string, CheckBox, int>> Coll_Checked_TwinCAT = new ObservableCollection<Tuple<string, CheckBox, int>>();

        ObservableCollection<Tuple<string, List<Tuple<string, string, string>>, StackPanel>> Coll_Tuple_Logo8 = new ObservableCollection<Tuple<string, List<Tuple<string, string, string>>, StackPanel>>();
        ObservableCollection<Tuple<string, List<Tuple<string, string, string>>, StackPanel>> Coll_Tuple_TiaPortal = new ObservableCollection<Tuple<string, List<Tuple<string, string, string>>, StackPanel>>();
        ObservableCollection<Tuple<string, List<Tuple<string, string, string>>, StackPanel>> Coll_Tuple_TwinCAT = new ObservableCollection<Tuple<string, List<Tuple<string, string, string>>, StackPanel>>();



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
            ProjekteLesen();

            // es muss zuerst InitializeComponent() laufen --> sonst sind die Referenzen leer 

            Eigenschaften_Logo8.Add(new AlleEigenschaften("PLC", Web_Logo8_PLC, StackPanel_Logo8_PLC, Button_Starten_Logo8_PLC));
            Eigenschaften_Logo8.Add(new AlleEigenschaften("BUG", Web_Logo8_PLC_Bugs, StackPanel_Logo8_PLC_Bugs, Button_Starten_Logo8_PLC_Bugs));
            
            AlleProgrammierSprachen_Logo8.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_Logo8_FUP));
            AlleProgrammierSprachen_Logo8.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_Logo8_KOP));


            List<Tuple<string, string, string>> LogoTuple = new List<Tuple<string, string, string>>();

            Coll_Html_TiaPortal.Add(new Tuple<string, WebBrowser>("DT", Web_TiaPortal_PLC_DT));
            Coll_Html_TiaPortal.Add(new Tuple<string, WebBrowser>("HMI", Web_TiaPortal_PLC_HMI));
            Coll_Html_TiaPortal.Add(new Tuple<string, WebBrowser>("FIO", Web_TiaPortal_PLC_FIO));
            Coll_Html_TiaPortal.Add(new Tuple<string, WebBrowser>("PLC", Web_TiaPortal_PLC));
            Coll_Html_TiaPortal.Add(new Tuple<string, WebBrowser>("BUG", Web_TiaPortal_PLC_Bugs));

            Coll_Html_TwinCAT.Add(new Tuple<string, WebBrowser>("DT", Web_TwinCAT_PLC_DT));
            Coll_Html_TwinCAT.Add(new Tuple<string, WebBrowser>("VISU", Web_TwinCAT_PLC_VISU));
            Coll_Html_TwinCAT.Add(new Tuple<string, WebBrowser>("NC", Web_TwinCAT_PLC_NC));
            Coll_Html_TwinCAT.Add(new Tuple<string, WebBrowser>("BUG", Web_TwinCAT_PLC_Bugs));
            Coll_Html_TwinCAT.Add(new Tuple<string, WebBrowser>("PLC", Web_TwinCAT_PLC));


            Coll_Checked_TiaPortal.Add(new Tuple<string, CheckBox, int>("FUP", Checkbox_TiaPortal_FUP, 4));
            Coll_Checked_TiaPortal.Add(new Tuple<string, CheckBox, int>("KOP", Checkbox_TiaPortal_KOP, 4));
            Coll_Checked_TiaPortal.Add(new Tuple<string, CheckBox, int>("SCL", Checkbox_TiaPortal_SCL, 4));

            Coll_Checked_TwinCAT.Add(new Tuple<string, CheckBox, int>("AS", Checkbox_TwinCAT_AS, 3));
            Coll_Checked_TwinCAT.Add(new Tuple<string, CheckBox, int>("AWL", Checkbox_TwinCAT_AWL, 4));
            Coll_Checked_TwinCAT.Add(new Tuple<string, CheckBox, int>("CFC", Checkbox_TwinCAT_CFC, 4));
            Coll_Checked_TwinCAT.Add(new Tuple<string, CheckBox, int>("FUP", Checkbox_TwinCAT_FUP, 4));
            Coll_Checked_TwinCAT.Add(new Tuple<string, CheckBox, int>("KOP", Checkbox_TwinCAT_KOP, 4));
            Coll_Checked_TwinCAT.Add(new Tuple<string, CheckBox, int>("ST", Checkbox_TwinCAT_ST, 3));
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

    }
}
