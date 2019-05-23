using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using System.Xml.Linq;

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
                Console.WriteLine("{0} Exception 1 caught.", exp);
            }
        }
    }

    public partial class MainWindow : Window
    {
        string LeereHtmlSeite = "<!doctype html>   </html >";

        bool Anzeige_Logo8_Aktualisieren = false;
        bool Anzeige_TiaPortal_Aktualisieren = false;
        bool Anzeige_TwinCAT_Aktualisieren = false;

        string Projekt_Logo8_Name = "";
        string Projekt_TiaPortal_Name = "";
        string Projekt_TwinCAT_Name = "";

        string ProjektOrdner_Logo8_Quelle = "";
        string ProjektOrdner_TiaPortal_Quelle = "";
        string ProjektOrdner_TwinCAT_Quelle = "";

        string Projekt_Logo8_Ziel = "h:\\Logo8V81";
        string Projekt_TiaPortal_Ziel = "h:\\TiaPortal_V14";
        string Projekt_TwinCAT_Ziel = "h:\\TwinCAT_V3";

        List<Button> Button_Logo8_Liste = new List<Button>();
        List<Button> Button_TiaPortal_Liste = new List<Button>();
        List<Button> Button_TwinCAT_Liste = new List<Button>();

        List<RadioButton> RadioButton_Logo8_List = new List<RadioButton>();
        List<RadioButton> RadioButton_TiaPortal_List = new List<RadioButton>();
        List<RadioButton> RadioButton_TwinCAT_List = new List<RadioButton>();

        public MainWindow()
        {
            InitializeComponent();
            EinstellungenLesen("Einstellungen.xml");
            ProjekteLesen();
        }

        public void ProjekteLesen()
        {
            Projekte_Logo8_Lesen();
            Projekte_TiaPortal_Lesen();
            Projekte_TwinCAT_Lesen();
        }


        public void EinstellungenLesen(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            foreach (XElement el in doc.Root.Elements())
            {
                switch (el.Name.LocalName)
                {
                    case "Logo": ProjektOrdner_Logo8_Quelle = el.Value.Trim(); break;
                    case "TiaPortal": ProjektOrdner_TiaPortal_Quelle = el.Value.Trim(); break;
                    case "TwinCAT": ProjektOrdner_TwinCAT_Quelle = el.Value.Trim(); break;
                    default: break;
                }
            }
        }



        private void DarstellungAendernListe(List<Button> KnopfListe, bool Enable, Color Farbe, string Text)
        {
            foreach (Button Knopf in KnopfListe)
            {
                Knopf.IsEnabled = Enable;
                Knopf.Background = new SolidColorBrush(Farbe);
                Knopf.Content = Text;
                Knopf.Refresh();
            }
        }


        private void AlleRadioButtonsDeaktivieren()
        {
            foreach (RadioButton R_Button in RadioButton_Logo8_List)
            {
                if (R_Button.IsChecked == true) R_Button.IsChecked = false;
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void ProjektStarten(object sender, RoutedEventArgs e)
        {
            string ProjektOeffnenMit = "";
            string ProjektOrdnerQuelle = "";
            string ProjektOrdnerZiel = "";
            string ProjektName = "";
            List<Button> Button_Liste = new List<Button>();

            Button StartKnopf = sender as Button;

            if (StartKnopf.Content == "Logo Projekt starten")
            {
                ProjektOrdnerQuelle = ProjektOrdner_Logo8_Quelle;
                ProjektOrdnerZiel = Projekt_Logo8_Ziel;
                Button_Liste = Button_Logo8_Liste;
                ProjektName = Projekt_Logo8_Name;
                ProjektOeffnenMit = "Projekt mit Logo8! öffnen";
            }
            else if (StartKnopf.Content == "TiaPortal Projekt starten")
            {
                ProjektOrdnerQuelle = ProjektOrdner_TiaPortal_Quelle;
                ProjektOrdnerZiel = Projekt_TiaPortal_Ziel;
                Button_Liste = Button_TiaPortal_Liste;
                ProjektName = Projekt_TiaPortal_Name;
                ProjektOeffnenMit = "Projekt mit TiaPortal öffnen";
            }
            else if (StartKnopf.Content == "TwinCAT Projekt starten")
            {
                ProjektOrdnerQuelle = ProjektOrdner_TwinCAT_Quelle;
                ProjektOrdnerZiel = Projekt_TwinCAT_Ziel;
                Button_Liste = Button_TwinCAT_Liste;
                ProjektName = Projekt_TwinCAT_Name;
                ProjektOeffnenMit = "Projekt mit TwinCAT öffnen";
            }
            else
            {
                MessageBox.Show("Unbekannter Knopf: " + StartKnopf.Content);
            }

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdnerQuelle);
            string sourceDirectory = ParentDirectory.FullName + "\\" + ProjektName;

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + ProjektOrdnerZiel + " löschen");
                if (System.IO.Directory.Exists(ProjektOrdnerZiel)) System.IO.Directory.Delete(ProjektOrdnerZiel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 2 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + ProjektOrdnerZiel + " erstellen");
                System.IO.Directory.CreateDirectory(ProjektOrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 3 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, ProjektOrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 4 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.LawnGreen, ProjektOeffnenMit);
                Process proc = new Process();
                proc.StartInfo.FileName = ProjektOrdnerZiel + "\\start.cmd";
                proc.StartInfo.WorkingDirectory = ProjektOrdnerZiel;
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 5 caught.", exp);
            }


        }

    }
}
