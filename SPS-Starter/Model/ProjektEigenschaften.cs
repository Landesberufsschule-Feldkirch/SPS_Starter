using System;

namespace SPS_Starter.Model
{
    public class ProjektEigenschaften
    {
        public SpsStarter.Steuerungen Steuerung { get; set; }
        public string QuellOrdner { get; set; }
        public string Bezeichnung { get; set; }
        public SpsStarter.SpsSprachen Programmiersprache { get; set; }
        public SpsStarter.SpsKategorie SpsKategorie { get; set; }



        public ProjektEigenschaften(MainWindow mw, SpsStarter.Steuerungen steuerung, string quelle)
        {
            Steuerung = steuerung;
            QuellOrdner = quelle;

            Bezeichnung = BezeichnungBestimmen(quelle);
            Programmiersprache = ProgrammierspracheBestimmen(mw, Steuerung, quelle);
            SpsKategorie = KategorieBestimmen(mw, Steuerung, quelle);
        }

        private SpsStarter.SpsKategorie KategorieBestimmen(MainWindow mw, SpsStarter.Steuerungen steuerung, string quelle)
        {
            foreach (var kategorien in mw.AlleWerte.AlleKategorien)
            {
                if (quelle.Contains(kategorien.Prefix)) return kategorien.Kategorie;
            }
            throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);
        }

        private SpsStarter.SpsSprachen ProgrammierspracheBestimmen(MainWindow mw, SpsStarter.Steuerungen steuerung, string quelle)
        {
            foreach (var sprache in mw.AlleWerte.AlleProgrammiersprachen)
            {
                if (quelle.Contains(sprache.Prefix)) return sprache.Sprache;
            }
            throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);
        }

        private string BezeichnungBestimmen(string quelle)
        {
            return "ölsdafj";
        }
    }
}