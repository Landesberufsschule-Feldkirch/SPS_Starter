using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SPS_Starter
{
    public partial class MainWindow
    {
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
                Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
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
            var Eigenschaften = gEigenschaften_Logo8;
            string ProjektOeffnenMit = "";
            string ProjektName = "";
            List<Button> Button_Liste = new List<Button>();

            Button StartKnopf = sender as Button;

            switch (StartKnopf?.Content.ToString())
            {
                case "Logo Projekt starten":
                    Eigenschaften = gEigenschaften_Logo8;
                    Button_Liste = gButton_Logo8;
                    ProjektName = gProjekt_Name;
                    ProjektOeffnenMit = "Projekt mit Logo8 öffnen";
                    break;

                case "TiaPortal Projekt starten":
                    Eigenschaften = gEigenschaften_TiaPortal;
                    Button_Liste = gButton_TiaPortal;
                    ProjektName = gProjekt_Name;
                    ProjektOeffnenMit = "Projekt mit TiaPortal öffnen";
                    break;

                case "TwinCAT Projekt starten":
                    Eigenschaften = gEigenschaften_TwinCAT;
                    Button_Liste = gButton_TwinCAT;
                    ProjektName = gProjekt_Name;
                    ProjektOeffnenMit = "Projekt mit TwinCAT öffnen";
                    break;

                default:
                    MessageBox.Show("Unbekannter Knopf: " + StartKnopf.Content);
                    break;
            }

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(Eigenschaften[0].OrdnerQuelle);
            string sourceDirectory = $@"{ParentDirectory.FullName}\{ProjektName}";

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + Eigenschaften[0].OrdnerZiel + " löschen");
                if (System.IO.Directory.Exists(Eigenschaften[0].OrdnerZiel)) System.IO.Directory.Delete(Eigenschaften[0].OrdnerZiel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 2 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Ordner " + Eigenschaften[0].OrdnerZiel + " erstellen");
                System.IO.Directory.CreateDirectory(Eigenschaften[0].OrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 3 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, Eigenschaften[0].OrdnerZiel);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 4 caught.");
            }

            try
            {
                DarstellungAendernListe(Button_Liste, true, Colors.LawnGreen, ProjektOeffnenMit);
                Process proc = new Process();
                proc.StartInfo.FileName = Eigenschaften[0].OrdnerZiel + "\\start.cmd";
                proc.StartInfo.WorkingDirectory = Eigenschaften[0].OrdnerZiel;
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 5 caught.");
            }

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl; // e.Source could have been used instead of sender as well
            TabItem item = tabControl.SelectedValue as TabItem;

            //Button_Starten_Logo8_PLC.Content = "Logo8 Projekt Starten";
            //Button_Starten_TiaPortal_PLC.Content = "TiaPortal Projekt Starten";
            //Button_Starten_TwinCAT_PLC.Content = "TwinCAT Projekt Starten";

            switch (item.Header.ToString())
            {
                case "Logo8":
                    if (gAnzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
                    break;

                case "TiaPortal":
                    if (gAnzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;

                case "TwinCAT":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;

                default:
                    break;
            }
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            switch (cb.Name)
            {
                case "Checkbox_Logo8_FUP":
                    if (gAnzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
                    break;
                case "Checkbox_Logo8_KOP":
                    if (gAnzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
                    break;

                case "Checkbox_TiaPortal_FUP":
                    if (gAnzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;
                case "Checkbox_TiaPortal_KOP":
                    if (gAnzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;
                case "Checkbox_TiaPortal_SCL":
                    if (gAnzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
                    break;


                case "Checkbox_TwinCAT_AS":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_AWL":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_CFC":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_FUP":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_KOP":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;
                case "Checkbox_TwinCAT_ST":
                    if (gAnzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
                    break;

                default:
                    break;

            }
        }

        public void ProjektbezeichnungenStackpanelLeerenAktualisieren(ObservableCollection<AlleEigenschaften> AlleEigenschaften, List<Button> ButtonListe)
        {
            foreach (var Eigenschaften in AlleEigenschaften)
            {
                Eigenschaften.ProjekteBezeichnung.Clear();// Zuerst die Listen löschen
                Eigenschaften.StackPanelBezeichnung.Children.Clear(); // Anzeige löschen
            }

            foreach (var Eigenschaften in AlleEigenschaften)
            {
                ButtonListe.Add(Eigenschaften.ButtonBezeichnung);
            }
        }

        public void ProgrammiersprachenListeAktualisieren(ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen, ObservableCollection<AlleEigenschaften> AlleEigenschaften)
        {
            string ProgrammierSprache = "";
            string ProjektTeilBezeichnung = "";
            int StartBezeichnung = 0;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(AlleEigenschaften[0].OrdnerQuelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {

                foreach (var Programmiersprachen in AlleProgrammierSprachen)
                {
                    if (d.Name.Contains(Programmiersprachen.Kurzbezeichnung))
                    {
                        if (Programmiersprachen.CheckBoxBezeichnung.IsChecked.Value)
                        {
                            ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                            StartBezeichnung = Programmiersprachen.Laenge + d.Name.IndexOf(Programmiersprachen.Kurzbezeichnung);

                            foreach (var Eigenschaften in AlleEigenschaften)
                            {
                                ProjektTeilBezeichnung = Eigenschaften.Kurzbezeichnung + "_" + Programmiersprachen.Kurzbezeichnung;
                                if (d.Name.Contains(ProjektTeilBezeichnung))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(d.Name.Substring(StartBezeichnung), ProgrammierSprache, d.Name);
                                    Eigenschaften.ProjekteBezeichnung.Add(TplEintrag);
                                }
                            }
                        }
                    }
                }
            }

        }

        public void AnzeigeAktualisieren(ObservableCollection<AlleEigenschaften> AlleEigenschaften)
        {
            string KurzbezeichnungSprache;

            foreach (var Eigenschaften in AlleEigenschaften)
            {
                //Eigenschaften.ProjekteBezeichnung.Sort();

                List<Tuple<string, string, string>> EindeutigeListe = Eigenschaften.ProjekteBezeichnung.Distinct().ToList();

                foreach (Tuple<string, string, string> Projekt in EindeutigeListe)
                {

                    if (Projekt.Item2 == "CPP")
                    {
                        KurzbezeichnungSprache = "C++";
                    }
                    else
                    {
                        KurzbezeichnungSprache = Projekt.Item2;
                    }

                    RadioButton rdo = new RadioButton
                    {
                        GroupName = Eigenschaften.GruppenName,
                        Name = Projekt.Item3,
                        FontSize = 14,
                        Content = Projekt.Item1 + " (" + KurzbezeichnungSprache + ")",
                        VerticalAlignment = VerticalAlignment.Top
                    };

                    if (Eigenschaften.GruppenName == "Logo8")
                    {
                        rdo.Checked += new RoutedEventHandler(Logo8_radioButton_Checked);
                    }

                    if (Eigenschaften.GruppenName == "TwinCAT")
                    {
                        rdo.Checked += new RoutedEventHandler(TwinCAT_radioButton_Checked);
                    }

                    if (Eigenschaften.GruppenName == "TIA_PORTAL_V14_SP1")
                    {
                        rdo.Checked += new RoutedEventHandler(TiaPortal_radioButton_Checked);
                    }

                    Eigenschaften.StackPanelBezeichnung.Children.Add(rdo);
                }

            }
        }

        public void WebBrowserFuellen(ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen, List<Button> KnopfListe, ObservableCollection<AlleEigenschaften> AlleEigenschaften)
        {
            string HtmlSeite;
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(AlleEigenschaften[0].OrdnerQuelle);

            DarstellungAendernListe(KnopfListe, true, Colors.Green, AlleEigenschaften[0].KnopfBeschriftung);

            string DateiName = $@"{ParentDirectory.FullName}\{gProjekt_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = gHtmlSeiteDateiFehlt;

            WebBrowserInhaltAnzeigen(AlleProgrammierSprachen, AlleEigenschaften, HtmlSeite);
        }

        public void WebBrowserInhaltAnzeigen(ObservableCollection<AlleProgrammierSprachen> AlleProgrammierSprachen, ObservableCollection<AlleEigenschaften> AlleEigenschaften, string HtmlSeite)
        {

            string ProgrammierSprache = "";

            foreach (var Programmiersprachen in AlleProgrammierSprachen)
            {
                if (gProjekt_Name.Contains(Programmiersprachen.Kurzbezeichnung))
                {
                    ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                }
            }


            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(HtmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(gHtmlSeiteLeer);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var EigenSchaften in AlleEigenschaften)
            {
                if (gProjekt_Name.Contains(EigenSchaften.Kurzbezeichnung + "_" + ProgrammierSprache))
                {
                    EigenSchaften.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
                }
                else
                {
                    EigenSchaften.BrowserBezeichnung.NavigateToStream(stmLeereHtmlSeite);
                }
            }

        }
    }
}