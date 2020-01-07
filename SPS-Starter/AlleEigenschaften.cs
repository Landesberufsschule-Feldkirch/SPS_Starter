using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SPS_Starter
{
    public class AlleEigenschaften
    {
        public string Kurzbezeichnung { get; set; }
        public string GruppenName { get; set; }
        public string KnopfBeschriftung { get; set; }
        public WebBrowser BrowserBezeichnung { get; set; }
        public StackPanel StackPanelBezeichnung { get; set; }
        public Button ButtonBezeichnung { get; set; }
        public List<Tuple<string, string, string>> ProjekteBezeichnung { get; set; }
        public string OrdnerQuelle { get; set; }
        public string OrdnerZiel { get; set; }
        public AlleEigenschaften(string Kurzbezeichnung, string GruppenName, string KnopfBeschriftung, WebBrowser BrowserBezeichnung, StackPanel StackPanelBezeichnung, Button ButtonBezeichnung, string OrdnerQuelle, string OrdnerZiel)
        {
            this.Kurzbezeichnung = Kurzbezeichnung;
            this.GruppenName = GruppenName;
            this.KnopfBeschriftung = KnopfBeschriftung;
            this.BrowserBezeichnung = BrowserBezeichnung;
            this.StackPanelBezeichnung = StackPanelBezeichnung;
            this.ButtonBezeichnung = ButtonBezeichnung;
            this.ProjekteBezeichnung = new List<Tuple<string, string, string>>();
            this.OrdnerQuelle = OrdnerQuelle;
            this.OrdnerZiel = OrdnerZiel;
        }
    }

}
