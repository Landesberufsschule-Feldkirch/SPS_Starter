using System.Collections.Generic;

namespace SPS_Starter.Model
{
    public class AlleWerte
    {
        public List<Programmiersprachen> AlleProgrammiersprachen { get; set; } = new List<Programmiersprachen>();
        public List<Kategorien> AlleKategorien { get; set; } = new List<Kategorien>();


        public AlleWerte()
        {
            AlleProgrammiersprachenEinlesen();
            AlleKategorienEinlesen();
        }

        private void AlleKategorienEinlesen()
        {
            AlleKategorien.Add(new Kategorien("ADS_", SpsStarter.SpsKategorie.AdsRemote));
            AlleKategorien.Add(new Kategorien("BUG_", SpsStarter.SpsKategorie.Bug));
            AlleKategorien.Add(new Kategorien("DT_", SpsStarter.SpsKategorie.DigitalTwin));
            AlleKategorien.Add(new Kategorien("FIO_", SpsStarter.SpsKategorie.FactoryIo));
            AlleKategorien.Add(new Kategorien("HMI_", SpsStarter.SpsKategorie.Hmi));
            AlleKategorien.Add(new Kategorien("NC_", SpsStarter.SpsKategorie.Nc));
            AlleKategorien.Add(new Kategorien("VISU_", SpsStarter.SpsKategorie.Visu));
            AlleKategorien.Add(new Kategorien("SNAP7_", SpsStarter.SpsKategorie.Snap7));
            AlleKategorien.Add(new Kategorien("TEST_", SpsStarter.SpsKategorie.SoftwareTests));
            AlleKategorien.Add(new Kategorien("PLC_", SpsStarter.SpsKategorie.Plc));
        }

        private void AlleProgrammiersprachenEinlesen()
        {
            AlleProgrammiersprachen.Add(new Programmiersprachen("AS_", "AS", SpsStarter.SpsSprachen.As));
            AlleProgrammiersprachen.Add(new Programmiersprachen("AWL_", "AWL", SpsStarter.SpsSprachen.Awl));
            AlleProgrammiersprachen.Add(new Programmiersprachen("CFC_", "CFC", SpsStarter.SpsSprachen.Cfc));
            AlleProgrammiersprachen.Add(new Programmiersprachen("CPP_", "C++", SpsStarter.SpsSprachen.Cpp));
            AlleProgrammiersprachen.Add(new Programmiersprachen("FUP_", "FUP", SpsStarter.SpsSprachen.Fup));
            AlleProgrammiersprachen.Add(new Programmiersprachen("KOP_", "KOP", SpsStarter.SpsSprachen.Kop));
            AlleProgrammiersprachen.Add(new Programmiersprachen("SCL_", "SCL", SpsStarter.SpsSprachen.Scl));
            AlleProgrammiersprachen.Add(new Programmiersprachen("ST_", "ST", SpsStarter.SpsSprachen.Stl));
        }
    }
}