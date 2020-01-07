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
        private void DarstellungAendernListe(List<Button> btnListe, bool enable, Color Farbe, string text)
        {
            foreach (Button Knopf in btnListe)
            {
                Knopf.IsEnabled = enable;
                Knopf.Background = new SolidColorBrush(Farbe);
                Knopf.Content = text;
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
            var Eigenschaften = gEigenschaften_TiaPortal;
            string ProjektOeffnenMit = "";
            string ProjektName = "";
            List<Button> Button_Liste = new List<Button>();

            Button StartKnopf = sender as Button;

            switch (StartKnopf?.Content.ToString())
            {
                case "Logo Projekt starten":
                    //Eigenschaften = gEigenschaften_Logo8;
                    //Button_Liste = gButton_Logo8;
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
                    if (gLogo8.AnzeigeAktualisieren) gLogo8.ProjekteLesen();
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
                    if (gLogo8 != null) if (gLogo8.AnzeigeAktualisieren) gLogo8.ProjekteLesen();
                    break;
                case "Checkbox_Logo8_KOP":
                    if (gLogo8 != null) if (gLogo8.AnzeigeAktualisieren) gLogo8.ProjekteLesen();
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

        public void ProjektbezeichnungenStackpanelLeerenAktualisieren(ObservableCollection<AlleEigenschaften> alleEigenschaften, List<Button> btnListe)
        {
            foreach (var Eigenschaften in alleEigenschaften)
            {
                Eigenschaften.ProjekteBezeichnung.Clear();// Zuerst die Listen löschen
                Eigenschaften.StackPanelBezeichnung?.Children?.Clear(); // Anzeige löschen
            }

            foreach (var Eigenschaften in alleEigenschaften)
            {
                btnListe.Add(Eigenschaften.ButtonBezeichnung);
            }
        }

        public void ProgrammiersprachenListeAktualisieren(ObservableCollection<AlleProgrammierSprachen> alleProgrammierSprachen, ObservableCollection<AlleEigenschaften> alleEigenschaften)
        {
            string ProgrammierSprache = "";
            string ProjektTeilBezeichnung = "";
            int StartBezeichnung = 0;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(alleEigenschaften[0].OrdnerQuelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {

                foreach (var Programmiersprachen in alleProgrammierSprachen)
                {
                    if (d.Name.Contains(Programmiersprachen.Kurzbezeichnung))
                    {
                        if (Programmiersprachen.CheckBoxBezeichnung.IsChecked.Value)
                        {
                            ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                            StartBezeichnung = Programmiersprachen.Laenge + d.Name.IndexOf(Programmiersprachen.Kurzbezeichnung);

                            foreach (var Eigenschaften in alleEigenschaften)
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

        public void AnzeigeAktualisieren(ObservableCollection<AlleEigenschaften> alleEigenschaften)
        {
            string KurzbezeichnungSprache;

            foreach (var Eigenschaften in alleEigenschaften)
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
                        rdo.Checked += new RoutedEventHandler(gLogo8.radioButton_Checked);
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

        public void WebBrowserFuellen(ObservableCollection<AlleProgrammierSprachen> alleProgrammierSprachen, List<Button> btnListe, ObservableCollection<AlleEigenschaften> alleEigenschaften)
        {
            string HtmlSeite;
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(alleEigenschaften[0].OrdnerQuelle);

            DarstellungAendernListe(btnListe, true, Colors.Green, alleEigenschaften[0].KnopfBeschriftung);

            string DateiName = $@"{ParentDirectory.FullName}\{gProjekt_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = gHtmlSeiteDateiFehlt;

            WebBrowserInhaltAnzeigen(alleProgrammierSprachen, alleEigenschaften, HtmlSeite);
        }

        public void WebBrowserInhaltAnzeigen(ObservableCollection<AlleProgrammierSprachen> alleProgrammierSprachen, ObservableCollection<AlleEigenschaften> alleEigenschaften, string htmlSeite)
        {

            string ProgrammierSprache = "";

            foreach (var Programmiersprachen in alleProgrammierSprachen)
            {
                if (gProjekt_Name.Contains(Programmiersprachen.Kurzbezeichnung))
                {
                    ProgrammierSprache = Programmiersprachen.Kurzbezeichnung;
                }
            }


            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(gHtmlSeiteLeer);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var EigenSchaften in alleEigenschaften)
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