using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SPS_Starter.Model
{
    public class TabEigenschaften
    {
        public SpsStarter.SpsKategorie SpsKategorie { get; set; }
        public SpsStarter.Steuerungen Steuerungen { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public StackPanel StackPanelBezeichnung { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public List<Tuple<string, string, string>> ProjekteBezeichnung { get; set; }

        public TabEigenschaften(SpsStarter.SpsKategorie spsKategorie, SpsStarter.Steuerungen steuerungen, WebBrowser browserBezeichnung, StackPanel stackPanelBezeichnung, Button buttonBezeichnung)
        {
            SpsKategorie = spsKategorie;
            Steuerungen = steuerungen;
            BrowserBezeichnung = browserBezeichnung;
            StackPanelBezeichnung = stackPanelBezeichnung;
            ButtonBezeichnung = buttonBezeichnung;
            ProjekteBezeichnung = new List<Tuple<string, string, string>>();
        }
    }

}
