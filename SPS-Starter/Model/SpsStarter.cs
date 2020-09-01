using System.Collections.Generic;
using System.IO;

namespace SPS_Starter.Model
{
    public class SpsStarter
    {
        public enum Steuerungen
        {
            Logo,
            TiaPortal,
            TwinCat
        }

        public enum SpsKategorie
        {
            AdsRemote,
            Bug,
            DigitalTwin,
            FactoryIo,
            Hmi,
            Nc,
            Plc,
            Snap7,
            SoftwareTests,
            Visu
        }

        public enum SpsSprachen
        {
            As,
            Awl,
            Cfc,
            Cpp,
            Fup,
            Kop,
            Scl,
            Stl
        }

    
        private readonly MainWindow _mainWindow;

        public SpsStarter(MainWindow mw)
        {
            _mainWindow = mw;
           
          
        }
    }
}