using System.Collections.Generic;

namespace SPS_Starter.Model
{
    public class AlleWerte
    {
        public Dictionary<SpsStarter.SpsSprachen, (string Prefix, string Anzeige)> AlleProgrammiersprachen { get; set; } = new Dictionary<SpsStarter.SpsSprachen, (string Prefix, string Anzeige)>();
        public Dictionary<SpsStarter.SpsKategorie, string> AlleKategorien { get; set; } = new Dictionary<SpsStarter.SpsKategorie, string>();

        public AlleWerte()
        {
            AlleProgrammiersprachenEinlesen();
            AlleKategorienEinlesen();
        }

        private void AlleKategorienEinlesen()
        {
            AlleKategorien.Add(SpsStarter.SpsKategorie.AdsRemote, "ADS_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.Bug, "BUG_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.DigitalTwin, "DT_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.FactoryIo, "FIO_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.Hmi, "HMI_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.Nc, "NC_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.Visu, "VISU_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.Snap7, "SNAP7_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.SoftwareTests, "TEST_");
            AlleKategorien.Add(SpsStarter.SpsKategorie.Plc, "PLC_");
        }

        private void AlleProgrammiersprachenEinlesen()
        {
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.As, (Prefix: "AS_", Anzeige: "AS"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Awl, (Prefix: "AWL_", Anzeige: "AWL"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Cfc, (Prefix: "CFC_", Anzeige: "CFC"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Cpp, (Prefix: "CPP_", Anzeige: "C++"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Fup, (Prefix: "FUP_", Anzeige: "FUP"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Kop, (Prefix: "KOP_", Anzeige: "KOP"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Scl, (Prefix: "SCL_", Anzeige: "SCL"));
            AlleProgrammiersprachen.Add(SpsStarter.SpsSprachen.Stl, (Prefix: "ST_", Anzeige: "ST"));
        }
    }
}