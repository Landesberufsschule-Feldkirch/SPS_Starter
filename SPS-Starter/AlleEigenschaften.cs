using System;
using System.Collections.Generic;
using System.Windows.Controls;

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

}
