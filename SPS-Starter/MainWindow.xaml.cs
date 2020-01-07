using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;

namespace SPS_Starter
{
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
        List<AlleEigenschaften> gEigenschaften_Logo8 = new List<AlleEigenschaften>();
        ObservableCollection<AlleEigenschaften> gEigenschaften_TiaPortal = new ObservableCollection<AlleEigenschaften>();
        ObservableCollection<AlleEigenschaften> gEigenschaften_TwinCAT = new ObservableCollection<AlleEigenschaften>();

        List<AlleProgrammierSprachen> gAlleProgrammierSprachen_Logo8 = new List<AlleProgrammierSprachen>();
        ObservableCollection<AlleProgrammierSprachen> gAlleProgrammierSprachen_TiaPortal = new ObservableCollection<AlleProgrammierSprachen>();
        ObservableCollection<AlleProgrammierSprachen> gAlleProgrammierSprachen_TwinCAT = new ObservableCollection<AlleProgrammierSprachen>();

        public string gHtmlSeiteLeer = "<!doctype html> Leere Seite?  </html >";
        public string gHtmlSeiteDateiFehlt = "<!doctype html> Datei fehlt!  </html >";

        //bool gAnzeige_Logo8_Aktualisieren = false;
        bool gAnzeige_TiaPortal_Aktualisieren = false;
        bool gAnzeige_TwinCAT_Aktualisieren = false;

        public string gProjekt_Name { get; set; }

        //List<Button> gButton_Logo8 = new List<Button>();
        List<Button> gButton_TiaPortal = new List<Button>();
        List<Button> gButton_TwinCAT = new List<Button>();

        EinstellungenOrdnerLesen einstellungenOrdner;

        public LogoSoft gLogo8;

        public MainWindow()
        {
            JsonLesen();
            InitializeComponent();

            InstanzenFuellen();

            gLogo8 = new LogoSoft(this, gEigenschaften_Logo8, gAlleProgrammierSprachen_Logo8, einstellungenOrdner.Logo, "Logo Projekt starten");


            gLogo8.ProjekteLesen();
            ProjekteLesen();
        }

        public void JsonLesen()
        {
            einstellungenOrdner = EinstellungenOrdnerLesen.FromJson(File.ReadAllText(@"Einstellungen.json"));
        }

        public void ProjekteLesen()
        {
            //  gLogo8.ProjekteLesen();
            Projekte_TiaPortal_Lesen();
            Projekte_TwinCAT_Lesen();
        }


        public void InstanzenFuellen()
        {
            string Logo8_Quelle = "";
            string TiaPortal_Quelle = "";
            string TwinCAT_Quelle = "";

            string Logo8_Ziel = "";
            string TiaPortal_Ziel = "";
            string TwinCAT_Ziel = "";

            XDocument doc = XDocument.Load("Einstellungen.xml");

            foreach (XElement el in doc.Root.Descendants())
            {
                switch (el.Name.LocalName)
                {
                    case "Source_Logo": Logo8_Quelle = el.Value.Trim(); break;
                    case "Source_TiaPortal": TiaPortal_Quelle = el.Value.Trim(); break;
                    case "Source_TwinCAT": TwinCAT_Quelle = el.Value.Trim(); break;
                    case "Destination_Logo": Logo8_Ziel = el.Value.Trim(); break;
                    case "Destination_TiaPortal": TiaPortal_Ziel = el.Value.Trim(); break;
                    case "Destination_TwinCAT": TwinCAT_Ziel = el.Value.Trim(); break;
                    default: break;
                }
            }

            Assert(Logo8_Quelle != "");
            Assert(TiaPortal_Quelle != "");
            Assert(TwinCAT_Quelle != "");
            Assert(Logo8_Ziel != "");
            Assert(TiaPortal_Ziel != "");
            Assert(TwinCAT_Ziel != "");

            gEigenschaften_Logo8.Add(new AlleEigenschaften("PLC", "Logo8", "Logo Projekt starten", Web_Logo8_PLC, StackPanel_Logo8_PLC, Button_Starten_Logo8_PLC, Logo8_Quelle, Logo8_Ziel));
            gEigenschaften_Logo8.Add(new AlleEigenschaften("BUG", "Logo8", "Logo Projekt starten", Web_Logo8_PLC_Bugs, StackPanel_Logo8_PLC_Bugs, Button_Starten_Logo8_PLC_Bugs, Logo8_Quelle, Logo8_Ziel));

            gEigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC", "TIA_PORTAL_V14_SP1", "TiaPortal Projekt starten", Web_TiaPortal_PLC, StackPanel_TiaPortal_PLC, Button_Starten_TiaPortal_PLC, TiaPortal_Quelle, TiaPortal_Ziel));
            gEigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_HMI", "TIA_PORTAL_V14_SP1", "TiaPortal Projekt starten", Web_TiaPortal_PLC_HMI, StackPanel_TiaPortal_PLC_HMI, Button_Starten_TiaPortal_PLC_HMI, TiaPortal_Quelle, TiaPortal_Ziel));
            gEigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_FIO", "TIA_PORTAL_V14_SP1", "TiaPortal Projekt starten", Web_TiaPortal_PLC_FIO, StackPanel_TiaPortal_PLC_FIO, Button_Starten_TiaPortal_PLC_FIO, TiaPortal_Quelle, TiaPortal_Ziel));
            gEigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_DT", "TIA_PORTAL_V14_SP1", "TiaPortal Projekt starten", Web_TiaPortal_PLC_DT, StackPanel_TiaPortal_PLC_DT, Button_Starten_TiaPortal_PLC_DT, TiaPortal_Quelle, TiaPortal_Ziel));
            gEigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_Snap7", "TIA_PORTAL_V14_SP1", "TiaPortal Projekt starten", Web_TiaPortal_PLC_Snap7, StackPanel_TiaPortal_PLC_Snap7, Button_Starten_TiaPortal_PLC_Snap7, TiaPortal_Quelle, TiaPortal_Ziel));
            gEigenschaften_TiaPortal.Add(new AlleEigenschaften("PLC_BUG", "TIA_PORTAL_V14_SP1", "TiaPortal Projekt starten", Web_TiaPortal_PLC_Bugs, StackPanel_TiaPortal_PLC_Bugs, Button_Starten_TiaPortal_PLC_Bugs, TiaPortal_Quelle, TiaPortal_Ziel));

            gEigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC", "TwinCAT", "TwinCAT Projekt starten", Web_TwinCAT_PLC, StackPanel_TwinCAT_PLC, Button_Starten_TwinCAT_PLC, TwinCAT_Quelle, TwinCAT_Ziel));
            gEigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_VISU", "TwinCAT", "TwinCAT Projekt starten", Web_TwinCAT_PLC_VISU, StackPanel_TwinCAT_PLC_VISU, Button_Starten_TwinCAT_PLC_VISU, TwinCAT_Quelle, TwinCAT_Ziel));
            gEigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_NC", "TwinCAT", "TwinCAT Projekt starten", Web_TwinCAT_PLC_NC, StackPanel_TwinCAT_PLC_NC, Button_Starten_TwinCAT_PLC_NC, TwinCAT_Quelle, TwinCAT_Ziel));
            gEigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_DT", "TwinCAT", "TwinCAT Projekt starten", Web_TwinCAT_PLC_DT, StackPanel_TwinCAT_PLC_DT, Button_Starten_TwinCAT_PLC_DT, TwinCAT_Quelle, TwinCAT_Ziel));
            gEigenschaften_TwinCAT.Add(new AlleEigenschaften("PLC_BUG", "TwinCAT", "TwinCAT Projekt starten", Web_TwinCAT_PLC_Bugs, StackPanel_TwinCAT_PLC_Bugs, Button_Starten_TwinCAT_PLC_Bugs, TwinCAT_Quelle, TwinCAT_Ziel));



            gAlleProgrammierSprachen_Logo8.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_Logo8_FUP));
            gAlleProgrammierSprachen_Logo8.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_Logo8_KOP));

            gAlleProgrammierSprachen_TiaPortal.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_TiaPortal_FUP));
            gAlleProgrammierSprachen_TiaPortal.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_TiaPortal_KOP));
            gAlleProgrammierSprachen_TiaPortal.Add(new AlleProgrammierSprachen("SCL", 4, Checkbox_TiaPortal_SCL));

            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("AS", 3, Checkbox_TwinCAT_AS));
            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("AWL", 4, Checkbox_TwinCAT_AWL));
            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("CFC", 4, Checkbox_TwinCAT_CFC));
            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("CPP", 4, Checkbox_TwinCAT_CPP));
            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("FUP", 4, Checkbox_TwinCAT_FUP));
            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("KOP", 4, Checkbox_TwinCAT_KOP));
            gAlleProgrammierSprachen_TwinCAT.Add(new AlleProgrammierSprachen("ST", 3, Checkbox_TwinCAT_ST));
        }

        private void Assert(bool v)
        {
            if (v == false)
            {
                throw new NotImplementedException();
            }
        }

    }
}
