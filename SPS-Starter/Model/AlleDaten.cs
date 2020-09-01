using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SPS_Starter.Model
{
    public class AlleDaten
    {
        public List<ProjektEigenschaften> AlleProjektEigenschaften { get; set; } = new List<ProjektEigenschaften>();
        public List<TabEigenschaften> AlleTabEigenschaften { get; set; } = new List<TabEigenschaften>();

     


        private readonly MainWindow _mainWindow;

        public AlleDaten(MainWindow mw)
        {
            _mainWindow = mw;
            
            var einstellungen = EinstellungenOrdnerLesen.FromJson(File.ReadAllText(@"Model//Einstellungen.json"));

          
            OrdnerEinlesen(_mainWindow, einstellungen.Logo.Source, einstellungen.Logo.Destination, SpsStarter.Steuerungen.Logo);
            OrdnerEinlesen(_mainWindow, einstellungen.TiaPortal.Source, einstellungen.TiaPortal.Destination, SpsStarter.Steuerungen.TiaPortal);
            OrdnerEinlesen(_mainWindow, einstellungen.TwinCat.Source, einstellungen.TwinCat.Destination, SpsStarter.Steuerungen.TwinCat);
            
            TabEigenschaftenEinlesen();
        }

    
        public void TabEigenschaftenEinlesen()
        {
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Plc, SpsStarter.Steuerungen.Logo, _mainWindow.WebLogoPlc, _mainWindow.StackPanelLogoPlc, _mainWindow.ButtonStartenLogoPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Bug, SpsStarter.Steuerungen.Logo, _mainWindow.WebLogoPlcBugs, _mainWindow.StackPanelLogoPlcBugs, _mainWindow.ButtonStartenLogoPlcBugs));

            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Plc, SpsStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlc, _mainWindow.StackPanelTiaPortalPlc, _mainWindow.ButtonStartenTiaPortalPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Hmi, SpsStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcHmi, _mainWindow.StackPanelTiaPortalPlcHmi, _mainWindow.ButtonStartenTiaPortalPlcHmi));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.FactoryIo, SpsStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcFio, _mainWindow.StackPanelTiaPortalPlcFio, _mainWindow.ButtonStartenTiaPortalPlcFio));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.DigitalTwin, SpsStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcDt, _mainWindow.StackPanelTiaPortalPlcDt, _mainWindow.ButtonStartenTiaPortalPlcDt));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Snap7, SpsStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcSnap7, _mainWindow.StackPanelTiaPortalPlcSnap7, _mainWindow.ButtonStartenTiaPortalPlcSnap7));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Bug, SpsStarter.Steuerungen.TiaPortal, _mainWindow.WebTiaPortalPlcBugs, _mainWindow.StackPanelTiaPortalPlcBugs, _mainWindow.ButtonStartenTiaPortalPlcBugs));

            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Plc, SpsStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlc, _mainWindow.StackPanelTwinCatPlc, _mainWindow.ButtonStartenTwinCatPlc));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Visu, SpsStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcVisu, _mainWindow.StackPanelTwinCatPlcVisu, _mainWindow.ButtonStartenTwinCatPlcVisu));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Nc, SpsStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcNc, _mainWindow.StackPanelTwinCatPlcNc, _mainWindow.ButtonStartenTwinCatPlcNc));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.DigitalTwin, SpsStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcDt, _mainWindow.StackPanelTwinCatPlcDt, _mainWindow.ButtonStartenTwinCatPlcDt));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.AdsRemote, SpsStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcAds, _mainWindow.StackPanelTwinCatPlcAds, _mainWindow.ButtonStartenTwinCatPlcAds));
            AlleTabEigenschaften.Add(new TabEigenschaften(SpsStarter.SpsKategorie.Bug, SpsStarter.Steuerungen.TwinCat, _mainWindow.WebTwinCatPlcBugs, _mainWindow.StackPanelTwinCatPlcBugs, _mainWindow.ButtonStartenTwinCatPlcBugs));
            }
        
        private void OrdnerEinlesen( MainWindow mw, string source, string destination, SpsStarter.Steuerungen steuerungen)
        {
            var parentDirectory = new DirectoryInfo(source);

            foreach (var ordnerInfo in parentDirectory.GetDirectories())
            {
                if ((ordnerInfo.Attributes & FileAttributes.Directory) != 0 && ordnerInfo.Name != ".git" && ordnerInfo.Name != "_SharedDll")
                {
                    AlleProjektEigenschaften.Add(new ProjektEigenschaften(mw, steuerungen, ordnerInfo.FullName, destination));
                }
            }
        }
    }
}