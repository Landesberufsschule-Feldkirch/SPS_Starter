using System.Collections.Generic;

namespace SPS_Starter.Model
{
    public class AlleWerte
    {
        public Dictionary<SpsStarter.SpsSprachen, (string Prefix, string Anzeige)> AlleProgrammiersprachen { get; set; } = new Dictionary<SpsStarter.SpsSprachen, (string Prefix, string Anzeige)>();
        public Dictionary<SpsStarter.SpsKategorie, (string Prefix, string Anzeige)> AlleKategorien { get; set; } = new Dictionary<SpsStarter.SpsKategorie, (string Prefix, string Anzeige)>();

        public AlleWerte()
        {
            AlleProgrammiersprachenEinlesen();
            AlleKategorienEinlesen();
        }

        private void AlleKategorienEinlesen()
        {
            AlleKategorien.Add(SpsStarter.SpsKategorie.AdsRemote, (Prefix: "ADS_", Anzeige: "ADS"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.Bug, (Prefix: "BUG_", Anzeige: "BUG"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.DigitalTwin, (Prefix: "DT_", Anzeige: "DT"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.FactoryIo, (Prefix: "FIO_", Anzeige: "FIO"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.Hmi, (Prefix: "HMI_", Anzeige: "HMI"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.Nc, (Prefix: "NC_", Anzeige: "NC"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.Visu, (Prefix: "VISU_", Anzeige: "VISU"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.Snap7, (Prefix: "SNAP7_", Anzeige: "SNAP7"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.SoftwareTests, (Prefix: "TEST_", Anzeige: "TEST"));
            AlleKategorien.Add(SpsStarter.SpsKategorie.Plc, (Prefix: "PLC_", Anzeige: "PLC"));
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