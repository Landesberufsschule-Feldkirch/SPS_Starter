using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace SPS_Starter
{
    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            try
            {
                uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 1 caught.");
            }
        }
    }

    public partial class MainWindow : Window
    {
        public string gHtmlSeiteLeer = "<!doctype html> Leere Seite?  </html >";
        public string gHtmlSeiteDateiFehlt = "<!doctype html> Datei fehlt!  </html >";

        public string gProjekt_Name { get; set; }

        public LogoSoft gLogo8;
        public TiaPortal gTiaPortal;
        public TwinCAT gTwinCat;

        public MainWindow()
        {
            InitializeComponent();

            var einstellungenOrdner = EinstellungenOrdnerLesen.FromJson(File.ReadAllText(@"Einstellungen.json"));

            gLogo8 = new LogoSoft(this, einstellungenOrdner.Logo);
            gTiaPortal = new TiaPortal(this, einstellungenOrdner.TiaPortal);
            gTwinCat = new TwinCAT(this, einstellungenOrdner.TwinCat);

            gLogo8.ProjekteLesen();
            gTiaPortal.ProjekteLesen();
            gTwinCat.ProjekteLesen();
        }
    }
}
