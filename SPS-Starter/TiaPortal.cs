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

namespace SPS_Starter
{

    public partial class MainWindow
    {
        public void Projekte_TiaPortal_Lesen()
        {

            // Zuerst die Listen löschen
            StackPanel_TiaPortal_PLC.Children.Clear();
            StackPanel_TiaPortal_PLC_Bugs.Children.Clear();
            StackPanel_TiaPortal_PLC_DT.Children.Clear();
            StackPanel_TiaPortal_PLC_FIO.Children.Clear();
            StackPanel_TiaPortal_PLC_HMI.Children.Clear();

            //
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_Bugs);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_DT);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_FIO);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_HMI);

            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_FIO = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_HMI = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_DT = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_BUG = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TiaPortal_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string Sprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;

                if (OrdnerName.Contains("FUP"))
                {
                    if (Checkbox_TiaPortal_FUP.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }
                    Sprache = "FUP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("FUP");
                }
                if (OrdnerName.Contains("KOP"))
                {
                    if (Checkbox_TiaPortal_KOP.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }
                    Sprache = "KOP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("KOP");
                }
                if (OrdnerName.Contains("SCL"))
                {
                    if (Checkbox_TiaPortal_SCL.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }
                    Sprache = "SCL";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("SCL");
                }

                if (Anzeigen)
                {
                    if (d.Name.Contains("PLC"))
                    {
                        if (d.Name.Contains("DT"))
                        {
                            Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                            TupleList_TiaPortal_PLC_DT.Add(TplEintrag);
                        }
                        else
                        {
                            if (d.Name.Contains("HMI"))
                            {
                                Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                                TupleList_TiaPortal_PLC_HMI.Add(TplEintrag);
                            }
                            else
                            {
                                if (d.Name.Contains("FIO"))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                                    TupleList_TiaPortal_PLC_FIO.Add(TplEintrag);
                                }
                                else
                                {
                                    // nur PLC und sonst nichts
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                                    TupleList_TiaPortal_PLC.Add(TplEintrag);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Es gibt momentan noch keine Gruppe bei den Bugs
                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                    TupleList_TiaPortal_PLC_BUG.Add(TplEintrag);
                }

            } // Ende foreach

            TupleList_TiaPortal_PLC.Sort();
            TupleList_TiaPortal_PLC_FIO.Sort();
            TupleList_TiaPortal_PLC_HMI.Sort();
            TupleList_TiaPortal_PLC_DT.Sort();
            TupleList_TiaPortal_PLC_BUG.Sort();

            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC, StackPanel_TiaPortal_PLC);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_FIO, StackPanel_TiaPortal_PLC_FIO);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_HMI, StackPanel_TiaPortal_PLC_HMI);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_DT, StackPanel_TiaPortal_PLC_DT);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_BUG, StackPanel_TiaPortal_PLC_Bugs);

            Anzeige_TiaPortal_Aktualisieren = true;
        }

        private void TiaPortal_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
            {
                RadioButton rdo = new RadioButton();
                rdo.GroupName = "TIA_PORTAL_V14_SP1";
                rdo.VerticalAlignment = VerticalAlignment.Top;
                rdo.Checked += new RoutedEventHandler(TiaPortal_radioButton_Checked);
                rdo.FontSize = 14;

                // nur PLC und sonst nichts
                rdo.Content = Projekt.Item1 + " (" + Projekt.Item2 + ")";
                rdo.Name = Projekt.Item3;
                StackPanel.Children.Add(rdo);
            }
        }

        private void TiaPortal_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TiaPortal_Quelle);

            DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.Green, "TiaPortal Projekt starten");
            Projekt_TiaPortal_Name = rb.Name;

            string LeereHtmlSeite = "<!doctype html>   </html >";
            string HtmlSeite = "";

            string DateiName = ParentDirectory.FullName + "\\" + rb.Name + "\\index.html";
            if (File.Exists(DateiName))
            {
                HtmlSeite = System.IO.File.ReadAllText(DateiName);
            }
            else
            {
                HtmlSeite = "<!doctype html>   </html >";
            }

            Web_TiaPortal_PLC.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_FIO.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_HMI.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_DT.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_Bugs.NavigateToString(LeereHtmlSeite);

            if (rb.Name.Contains("PLC"))
            {
                if (rb.Name.Contains("DT"))
                    Web_TiaPortal_PLC_DT.NavigateToString(HtmlSeite);
                else
                {
                    if (rb.Name.Contains("HMI"))
                        Web_TiaPortal_PLC_HMI.NavigateToString(HtmlSeite);
                    else
                    {
                        if (rb.Name.Contains("FIO"))
                            Web_TiaPortal_PLC_FIO.NavigateToString(HtmlSeite);
                        else
                        {
                            Web_TiaPortal_PLC.NavigateToString(HtmlSeite);
                        }
                    }
                }
            }
            else
            {
                if (rb.Name.Contains("BUG")) Web_TiaPortal_PLC_Bugs.NavigateToString(HtmlSeite);
                //bei Bug gibt es keine Unterkategorien
            }

        }

        private void TiaPortal_ProjektStarten(object sender, RoutedEventArgs e)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo("Projekte");
            string sourceDirectory = ParentDirectory.FullName + "\\" + Projekt_TiaPortal_Name;

            try
            {
                DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.Yellow, "Ordner " + Projekt_TiaPortal_Ziel + " löschen");
                if (System.IO.Directory.Exists(Projekt_TiaPortal_Ziel)) System.IO.Directory.Delete(Projekt_TiaPortal_Ziel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 2 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.Yellow, "Ordner " + Projekt_TiaPortal_Ziel + " erstellen");
                System.IO.Directory.CreateDirectory(Projekt_TiaPortal_Ziel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 3 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, Projekt_TiaPortal_Ziel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 4 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.LawnGreen, "Projekt mit TIA Portal V14 öffnen");
                Process proc = new Process();
                proc.StartInfo.FileName = Projekt_TiaPortal_Ziel + "\\start.cmd";
                proc.StartInfo.WorkingDirectory = Projekt_TiaPortal_Ziel;
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 5 caught.", exp);
            }

        }

        private void Button_Starten_TiaPortal_PLC_Click(object sender, RoutedEventArgs e) { }

        private void Checkbox_TiaPortal_FUP_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
        }
        private void Checkbox_TiaPortal_KOP_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
        }
        private void Checkbox_TiaPortal_SCL_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TiaPortal_Aktualisieren) Projekte_TiaPortal_Lesen();
        }
    }
}