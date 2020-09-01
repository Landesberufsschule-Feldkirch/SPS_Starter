using System;
using System.Linq;
using System.Windows;

namespace SPS_Starter.Model
{
    public class ProjektEigenschaften
    {
        public static int LaufendeNummer { get; set; }
        public SpsStarter.Steuerungen Steuerung { get; set; }
        public string QuellOrdner { get; set; }
        public  string ZielOrdner { get; set; }
        public string Bezeichnung { get; set; }
        public SpsStarter.SpsSprachen Programmiersprache { get; set; }
        public SpsStarter.SpsKategorie SpsKategorie { get; set; }



        public ProjektEigenschaften(MainWindow mw, SpsStarter.Steuerungen steuerung, string quelle, string ziel)
        {
            LaufendeNummer++;
            Steuerung = steuerung;
            QuellOrdner = quelle;
            ZielOrdner = ziel;

            Programmiersprache = ProgrammierspracheBestimmen(mw, Steuerung, quelle);
            SpsKategorie = KategorieBestimmen(mw, Steuerung, quelle);
            Bezeichnung = BezeichnungBestimmen(mw, quelle, SpsKategorie);
        }

        private SpsStarter.SpsKategorie KategorieBestimmen(MainWindow mw, SpsStarter.Steuerungen steuerung, string quelle)
        {
            foreach (var kategorien in mw.AlleWerte.AlleKategorien.Where(kategorien => quelle.Contains(kategorien.Prefix)))
            {
                return kategorien.Kategorie;
            }

            MessageBox.Show("Bezeichnungsproblem: " + quelle);
            return SpsStarter.SpsKategorie.AdsRemote;
        }

        private SpsStarter.SpsSprachen ProgrammierspracheBestimmen(MainWindow mw, SpsStarter.Steuerungen steuerung, string quelle)
        {
            foreach (var sprache in mw.AlleWerte.AlleProgrammiersprachen.Where(sprache => quelle.Contains(sprache.Prefix)))
                return sprache.Sprache;

            MessageBox.Show("Bezeichnungsproblem: " + quelle);
            return SpsStarter.SpsSprachen.As;
        }

        private string BezeichnungBestimmen(MainWindow mw, string quelle, SpsStarter.SpsKategorie kategorie)
        {
            var prefix = "_";

            foreach (var kategorien in mw.AlleWerte.AlleKategorien.Where(kategorien => kategorien.Kategorie == kategorie))
            {
                prefix = kategorien.Prefix;
            }


            var pos = quelle.IndexOf(prefix, StringComparison.Ordinal);
            var laenge = prefix.Length;

            return quelle.Substring(pos + laenge);
        }
    }
}