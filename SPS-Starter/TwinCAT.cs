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
        public void Projekte_TwinCAT_Lesen()
        {

            // Zuerst die Listen löschen
            StackPanel_TwinCAT_PLC.Children.Clear();
            StackPanel_TwinCAT_PLC_Bugs.Children.Clear();
            StackPanel_TwinCAT_PLC_DT.Children.Clear();
            StackPanel_TwinCAT_PLC_NC.Children.Clear();
            StackPanel_TwinCAT_PLC_VISU.Children.Clear();

            //
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_Bugs);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_DT);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_NC);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_VISU);


            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_VISU = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_NC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_BUG = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_DT = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TwinCAT_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string Sprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;

                if (OrdnerName.Contains("AS"))
                {
                    if (Checkbox_TwinCAT_AS.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }

                    Sprache = "AS/SFC";
                    StartBezeichnung = 3 + OrdnerName.IndexOf("AS");
                }
                if (OrdnerName.Contains("AWL"))
                {
                    if (Checkbox_TwinCAT_AWL.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }

                    Sprache = "AWL/IL";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("AWL");
                }
                if (OrdnerName.Contains("CFC"))
                {
                    if (Checkbox_TwinCAT_CFC.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }

                    Sprache = "CFC";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("CFC");
                }
                if (OrdnerName.Contains("FUP"))
                {
                    if (Checkbox_TwinCAT_FUP.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }

                    Sprache = "FUP/FBD";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("FUP");
                }
                if (OrdnerName.Contains("KOP"))
                {
                    if (Checkbox_TwinCAT_KOP.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }

                    Sprache = "KOP/LD";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("KOP");
                }
                if (OrdnerName.Contains("ST"))
                {
                    if (Checkbox_TwinCAT_ST.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }

                    Sprache = "ST";
                    StartBezeichnung = 3 + OrdnerName.IndexOf("ST");
                }

                if (Anzeigen)
                {

                    if (d.Name.Contains("PLC"))
                    {
                        if (d.Name.Contains("DT"))
                        {
                            Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                            TupleList_TwinCAT_PLC_DT.Add(TplEintrag);
                        }
                        else
                        {
                            if (d.Name.Contains("VISU"))
                            {
                                Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                                TupleList_TwinCAT_PLC_VISU.Add(TplEintrag);
                            }
                            else
                            {
                                if (d.Name.Contains("NC"))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                                    TupleList_TwinCAT_PLC_NC.Add(TplEintrag);
                                }
                                else
                                {
                                    // nur PLC und sonst nichts
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                                    TupleList_TwinCAT_PLC.Add(TplEintrag);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Es gibt momentan noch keine Gruppe bei den Bugs
                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                    TupleList_TwinCAT_PLC_BUG.Add(TplEintrag);
                }
            } // Ende foreach

            TupleList_TwinCAT_PLC.Sort();
            TupleList_TwinCAT_PLC_NC.Sort();
            TupleList_TwinCAT_PLC_VISU.Sort();
            TupleList_TwinCAT_PLC_DT.Sort();
            TupleList_TwinCAT_PLC_BUG.Sort();

            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC, StackPanel_TwinCAT_PLC);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_NC, StackPanel_TwinCAT_PLC_NC);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_VISU, StackPanel_TwinCAT_PLC_VISU);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_DT, StackPanel_TwinCAT_PLC_DT);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_BUG, StackPanel_TwinCAT_PLC_Bugs);

            Anzeige_TwinCAT_Aktualisieren = true;
        }


        private void TwinCAT_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
            {
                RadioButton rdo = new RadioButton();
                rdo.GroupName = "TwinCAT";
                rdo.VerticalAlignment = VerticalAlignment.Top;
                rdo.Checked += new RoutedEventHandler(Logo8_radioButton_Checked);
                rdo.FontSize = 14;

                // nur PLC und sonst nichts
                rdo.Content = Projekt.Item1 + " (" + Projekt.Item2 + ")";
                rdo.Name = Projekt.Item3;
                StackPanel.Children.Add(rdo);
            }
        }

        private void TwinCAT_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TwinCAT_Quelle);

            DarstellungAendernListe(Button_TwinCAT_Liste, true, Colors.Green, "Projekt starten");
            Projekt_TwinCAT_Name = rb.Name;

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


            Web_TwinCAT_PLC.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_VISU.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_NC.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_DT.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_Bugs.NavigateToString(LeereHtmlSeite);

            if (rb.Name.Contains("PLC"))
            {
                if (rb.Name.Contains("DT"))
                    Web_TwinCAT_PLC.NavigateToString(HtmlSeite);
                else
                {
                    if (rb.Name.Contains("VISU"))
                        Web_TwinCAT_PLC_VISU.NavigateToString(HtmlSeite);
                    else
                    {
                        if (rb.Name.Contains("NC")) Web_TwinCAT_PLC_NC.NavigateToString(HtmlSeite);
                        else Web_TwinCAT_PLC.NavigateToString(HtmlSeite);
                    }
                }
            }
            else
            {
                if (rb.Name.Contains("BUG")) Web_TwinCAT_PLC_Bugs.NavigateToString(HtmlSeite);
                //bei Bug gibt es keine Unterkategorien
            }
        }

        private void TwinCAT_ProjektStarten(object sender, RoutedEventArgs e)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TwinCAT_Quelle);
            string sourceDirectory = ParentDirectory.FullName + "\\" + Projekt_TwinCAT_Name;

            try
            {
                DarstellungAendernListe(Button_TwinCAT_Liste, true, Colors.Yellow, "Ordner " + Projekt_TiaPortal_Ziel + " löschen");
                if (System.IO.Directory.Exists(Projekt_TiaPortal_Ziel)) System.IO.Directory.Delete(Projekt_TiaPortal_Ziel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 2 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_TwinCAT_Liste, true, Colors.Yellow, "Ordner " + Projekt_TiaPortal_Ziel + " erstellen");
                System.IO.Directory.CreateDirectory(Projekt_TiaPortal_Ziel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 3 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_TwinCAT_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, Projekt_TiaPortal_Ziel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 4 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_TwinCAT_Liste, true, Colors.LawnGreen, "Projekt mit TwinCAT V3 öffnen");
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




        private void Button_Starten_TwinCAT_PLC_Click(object sender, RoutedEventArgs e) { }

        private void Checkbox_TwinCAT_AS_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
        }
        private void Checkbox_TwinCAT_AWL_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
        }
        private void Checkbox_TwinCAT_CFC_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
        }
        private void Checkbox_TwinCAT_FUP_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
        }
        private void Checkbox_TwinCAT_KOP_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
        }
        private void Checkbox_TwinCAT_ST_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_TwinCAT_Aktualisieren) Projekte_TwinCAT_Lesen();
        }
    }
}