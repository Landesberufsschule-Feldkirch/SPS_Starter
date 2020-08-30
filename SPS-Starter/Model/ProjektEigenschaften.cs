using System;
using System.Collections.Generic;
using System.Text;

namespace SPS_Starter.Model
{
 public   class ProjektEigenschaften
    {
        public SpsStarter.Steuerungen Steuerung { get; set; }
        public string QuellOrdner { get; set; }
        public  string Bezeichnung { get; set; }
        public string Programmiersprache { get; set; }


        public ProjektEigenschaften(SpsStarter.Steuerungen steuerung, string quelle   )
        {
            Steuerung = steuerung;
            QuellOrdner = quelle;

            Bezeichnung = BezeichnungBestimmen(quelle);
            Programmiersprache = ProgrammierspracheBestimmen(Steuerung, quelle);
        }

        private string ProgrammierspracheBestimmen(SpsStarter.Steuerungen steuerung, string quelle)
        {
            return "kop";
        }

        private string BezeichnungBestimmen(string quelle)
        {
            return "ölsdafj";
        }
    }
}