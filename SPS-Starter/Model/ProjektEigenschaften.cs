using System;

namespace SPS_Starter.Model
{
    public class ProjektEigenschaften
    {
        public SpsStarter.Steuerungen Steuerung { get; set; }
        public string QuellOrdner { get; set; }
        public string Bezeichnung { get; set; }
        public SpsStarter.Programmiersprachen Programmiersprache { get; set; }
        public SpsStarter.Kategorien Kategorien { get; set; }


        public ProjektEigenschaften(SpsStarter.Steuerungen steuerung, string quelle)
        {
            Steuerung = steuerung;
            QuellOrdner = quelle;

            Bezeichnung = BezeichnungBestimmen(quelle);
            Programmiersprache = ProgrammierspracheBestimmen(Steuerung, quelle);
            Kategorien = KategorieBestimmen(Steuerung, quelle);
        }

        private SpsStarter.Kategorien KategorieBestimmen(SpsStarter.Steuerungen steuerung, string quelle)
        {
            switch (steuerung)
            {
                case SpsStarter.Steuerungen.Logo:
                    return quelle.Contains("BUG_") ? SpsStarter.Kategorien.Bug : SpsStarter.Kategorien.Plc;

                case SpsStarter.Steuerungen.TiaPortal:
                    if (quelle.Contains("FIO_")) return SpsStarter.Kategorien.FactoryIo;
                    if (quelle.Contains("DT_")) return SpsStarter.Kategorien.DigitalTwin;
                    if (quelle.Contains("HMI_")) return SpsStarter.Kategorien.Hmi;
                    return quelle.Contains("Snap7_") ? SpsStarter.Kategorien.Snap7 : SpsStarter.Kategorien.Plc;

                case SpsStarter.Steuerungen.TwinCat:

                    if (quelle.Contains("VISU_")) return SpsStarter.Kategorien.Visu;
                    if (quelle.Contains("ADS_")) return SpsStarter.Kategorien.AdsRemote;
                    if (quelle.Contains("DT_")) return SpsStarter.Kategorien.DigitalTwin;
                    return quelle.Contains("NC_") ? SpsStarter.Kategorien.Nc : SpsStarter.Kategorien.Plc;

                default:
                    throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);
            }
        }

        private SpsStarter.Programmiersprachen ProgrammierspracheBestimmen(SpsStarter.Steuerungen steuerung, string quelle)
        {
            switch (steuerung)
            {
                case SpsStarter.Steuerungen.Logo:
                    if (quelle.Contains("FUP_")) return SpsStarter.Programmiersprachen.Fup;
                    if (quelle.Contains("KOP_")) return SpsStarter.Programmiersprachen.Kop;
                    throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);

                case SpsStarter.Steuerungen.TiaPortal:
                    if (quelle.Contains("FUP_")) return SpsStarter.Programmiersprachen.Fup;
                    if (quelle.Contains("KOP_")) return SpsStarter.Programmiersprachen.Kop;
                    if (quelle.Contains("SCL_")) return SpsStarter.Programmiersprachen.Scl;
                    throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);

                case SpsStarter.Steuerungen.TwinCat:
                    if (quelle.Contains("AS_")) return SpsStarter.Programmiersprachen.As;
                    if (quelle.Contains("AWL_")) return SpsStarter.Programmiersprachen.Awl;
                    if (quelle.Contains("CFC_")) return SpsStarter.Programmiersprachen.Cfc;
                    if (quelle.Contains("CPP_")) return SpsStarter.Programmiersprachen.Cpp;
                    if (quelle.Contains("FUP_")) return SpsStarter.Programmiersprachen.Fup;
                    if (quelle.Contains("KOP_")) return SpsStarter.Programmiersprachen.Kop;
                    if (quelle.Contains("ST_")) return SpsStarter.Programmiersprachen.Stl;
                    throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);

                default:
                    throw new ArgumentOutOfRangeException(nameof(steuerung), steuerung, null);
            }
        }

        private string BezeichnungBestimmen(string quelle)
        {
            return "ölsdafj";
        }
    }
}